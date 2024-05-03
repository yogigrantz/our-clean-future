﻿using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OurCleanFuture.App.Extensions;
using OurCleanFuture.Data;
using OurCleanFuture.Data.Entities;

namespace OurCleanFuture.App.Services;

public class DataExportService
{
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
    private readonly IFileLoggerService _logger = null;

    public DataExportService(IDbContextFactory<AppDbContext> dbContextFactory, IFileLoggerService logger)
    {
        _dbContextFactory = dbContextFactory;
        _logger = logger;
    }

    public async Task<XlsxExport> GenerateActionsXlsx(string fileName)
    {
        XlsxExport xlsxExport = new XlsxExport(fileName);

        try
        {
            using var context = _dbContextFactory.CreateDbContext();
            var actions = await MapActionsToExportModel(context);

            var orderedActions = actions
                .OrderBy(a => a.Number[0])
                .ThenBy(a => a.Number.Length)
                .ThenBy(a => a.Number);

            using (var workbook = new XLWorkbook())
            {
                var ws = workbook.Worksheets.Add("Actions");

                SetHeaderNames(ws);
                SetHeaderStyle(ws);
                InsertData(orderedActions, ws);
                SetColumnWidth(ws);
                SetHyperlinksForIdColumn(ws);
                SetHyperlinksForEditColumn(ws);
                SetBackgroundColorForIdAndNumberColumn(ws);
                SetGreyColorForAlternatingRows(ws);

                xlsxExport.Save(workbook);
            }

        }
        catch (Exception ex)
        {
            _logger.Log(ex.Message);
        }

        return xlsxExport;
    }

    private async Task<ActionExportModel[]> MapActionsToExportModel(AppDbContext context)
    {
        List<ActionExportModel> actionExportModels = new List<ActionExportModel>();
        Data.Entities.Action curActionRec = null;
        List<Data.Entities.Action> actions = await context.Actions
                                        .Include(l => l.Leads)
                                        .ThenInclude(b => b.Branch)
                                        .ThenInclude(d => d.Department)
                                        .Include(o => o.Objective)
                                        .Include(dc => dc.DirectorsCommittees)
                                        .Include(u => u.UndertakenInTheTraditionalTerritoriesOf)
                                        .Include(i => i.Indicators)
                                        .ToListAsync();
        foreach (Data.Entities.Action a in actions)
        {
            try
            {
                curActionRec = a;
                string? leadDept = a.Leads?.FirstOrDefault()?.Branch?.Department?.ShortName;
                string? branch = a.Leads?.FirstOrDefault()?.Branch?.Name;
                string dirComitee1 = "";
                string dirComitee2 = "";
                if (a.DirectorsCommittees?.Count >= 1)
                    dirComitee1 = a.DirectorsCommittees[0].Name;
                if (a.DirectorsCommittees?.Count >= 2)
                    dirComitee2 = a.DirectorsCommittees[1].Name;

                DateOnly? targetCompletionDate = null;
                if (a.TargetCompletionDate != null)
                    targetCompletionDate = DateOnly.FromDateTime((System.DateTime)a.TargetCompletionDate);
                DateOnly? actuaCompletionDate = null;
                if (a.ActualCompletionDate != null)
                    actuaCompletionDate = DateOnly.FromDateTime((System.DateTime)a.ActualCompletionDate);

                string undertaken = "";
                if (a.UndertakenInTheTraditionalTerritoriesOf != null)
                    undertaken = string.Join(", ", a.UndertakenInTheTraditionalTerritoriesOf.Select(x => x.AbbreviatedName));

                string indicators = "";
                if (a.Indicators != null && a.Indicators.Any(i => i.CollectionInterval == Data.Entities.CollectionInterval.Quarterly
                                                               || i.CollectionInterval == Data.Entities.CollectionInterval.Biannual))
                    indicators = string.Join(
                                "\n\n",
                                a.Indicators
                                    .Where(i => i.CollectionInterval == Data.Entities.CollectionInterval.Quarterly
                                             || i.CollectionInterval == Data.Entities.CollectionInterval.Biannual)
                                    .Select(x => x.Title));
                string indicatorsAnnual = "";
                if (a.Indicators != null && a.Indicators.Any(i => i.CollectionInterval == Data.Entities.CollectionInterval.Annual))
                    indicatorsAnnual = string.Join(
                                "\n\n",
                                a.Indicators
                                    .Where(i => i.CollectionInterval == Data.Entities.CollectionInterval.Annual)
                                    .Select(x => x.Title)
                            );

                string internalStatus;
                try
                {
                    internalStatus = a.InternalStatus.GetDisplayName();

                }
                catch (Exception)
                {
                    internalStatus = "NotStarted";
                }
                string externalStatus;
                try
                {
                    externalStatus = a.ExternalStatus.GetDisplayName();
                }
                catch (Exception)
                {
                    externalStatus = "NotStarted";
                }

                ActionExportModel aem = new ActionExportModel(a.Id, a.Number, a.Title, leadDept ?? "", branch ?? "", a.Objective?.Title ?? "",
                            dirComitee1, dirComitee2, targetCompletionDate, actuaCompletionDate,
                            internalStatus,
                            a.InternalStatusUpdatedBy,
                            a.InternalStatusUpdatedDate,
                            externalStatus,
                            a.ExternalStatusUpdatedBy,
                            a.ExternalStatusUpdatedDate,
                            undertaken,
                            a.EngagementsAndPartnershipActivities,
                            a.PublicExplanation,
                            a.Note,
                            indicators,
                            indicatorsAnnual,
                            "edit");

                actionExportModels.Add(aem);

            }
            catch (Exception ex)
            {
                _logger.Log($"Failed record: {ex.Message} {ex.InnerException?.Message}\r\n{JsonConvert.SerializeObject(curActionRec, Formatting.Indented)}");
            }
        }

        return actionExportModels.ToArray();

    }

    private static void SetHyperlinksForIdColumn(IXLWorksheet ws)
    {
        var linkColumn = ws.Column("A");
        foreach (var cell in linkColumn.Cells().Skip(1))
        {
            cell.SetHyperlink(
                new XLHyperlink(
                    $"https://ourcleanfuture.ynet.gov.yk.ca/actions/details/{cell.Value}"
                )
            );
        }
    }

    private static void SetHyperlinksForEditColumn(IXLWorksheet ws)
    {
        var linkColumn = ws.Column("W");
        var idColumn = ws.Column("A");
        foreach (var idCell in idColumn.Cells().Skip(1))
        {
            var id = idCell.Value;
            var linkCell = linkColumn.Cell(idCell.Address.RowNumber);

            linkCell.SetHyperlink(
                new XLHyperlink($"https://ourcleanfuture.ynet.gov.yk.ca/actions/edit/{id}")
            );
        }
    }

    private static void SetColumnWidth(IXLWorksheet ws)
    {
        ws.Column("A").AdjustToContents();
        ws.Column("B").AdjustToContents();
        ws.Column("C").Width = 20;
        ws.Column("D").Width = 10;
        ws.Column("E").Width = 15;
        ws.Column("F").Width = 15;
        ws.Column("G").Width = 15;
        ws.Column("H").Width = 15;
        ws.Column("I").Width = 10;
        ws.Column("J").Width = 10;
        ws.Column("K").Width = 10;
        ws.Column("L").Width = 10;
        ws.Column("M").AdjustToContents();
        ws.Column("N").Width = 10;
        ws.Column("O").Width = 10;
        ws.Column("P").AdjustToContents();
        ws.Column("Q").Width = 20;
        ws.Column("R").Width = 30;
        ws.Column("S").Width = 30;
        ws.Column("T").Width = 30;
        ws.Column("U").Width = 20;
        ws.Column("V").Width = 20;
        ws.Column("W").AdjustToContents();
        ws.Columns().Style.Alignment.WrapText = true;
    }

    private static void SetHeaderNames(IXLWorksheet ws)
    {
        ws.Cell("A1").Value = "Id";
        ws.Cell("B1").Value = "Number";
        ws.Cell("C1").Value = "Name";
        ws.Cell("D1").Value = "Lead Department";
        ws.Cell("E1").Value = "Branch";
        ws.Cell("F1").Value = "Objective";
        ws.Cell("G1").Value = "Directors Committee1";
        ws.Cell("H1").Value = "Directors Committee2";
        ws.Cell("I1").Value = "Target Completion Date";
        ws.Cell("J1").Value = "Actual Completion Date";
        ws.Cell("K1").Value = "Internal Status";
        ws.Cell("L1").Value = "Updated By";
        ws.Cell("M1").Value = "Updated Date";
        ws.Cell("N1").Value = "External Status";
        ws.Cell("O1").Value = "Updated By";
        ws.Cell("P1").Value = "Updated Date";
        ws.Cell("Q1").Value = "Traditional Territories";
        ws.Cell("R1").Value = "Engagements And Partnership Activities";
        ws.Cell("S1").Value = "Public Explanation";
        ws.Cell("T1").Value = "Additional Internal Info";
        ws.Cell("U1").Value = "Quarterly Or Biannual Indicators";
        ws.Cell("V1").Value = "Annual Indicators";
        ws.Cell("W1").Value = "Edit Link";
    }

    private static void InsertData(
        IOrderedEnumerable<ActionExportModel> orderedActions,
        IXLWorksheet ws
    ) => ws.Cell("A2").InsertData(orderedActions);

    private static void SetHeaderStyle(IXLWorksheet ws)
    {
        ws.RangeUsed().SetAutoFilter();
        ws.Row(1).RowUsed().Style.Fill.BackgroundColor = XLColor.FromArgb(0x90AFC5);
        ws.SheetView.FreezeRows(1);
    }

    private static void SetGreyColorForAlternatingRows(IXLWorksheet ws)
    {
        foreach (var row in ws.Range("C2:W300").RowsUsed())
        {
            if (row.RowNumber() % 2 == 0)
            {
                row.Style.Fill.BackgroundColor = XLColor.White;
            }
            else
            {
                row.Style.Fill.BackgroundColor = XLColor.FromArgb(0xF2F2F2);
            }
        }
    }

    private static void SetBackgroundColorForIdAndNumberColumn(IXLWorksheet worksheet)
    {
        var columnA = worksheet.Column("A");
        var columnB = worksheet.Column("B");

        foreach (var cell in columnB.Cells().Skip(1))
        {
            var prefix = cell.Value.ToString().ToUpper()[0];

            cell.Style.Fill.BackgroundColor = prefix switch
            {
                'C' => XLColor.FromArgb(0xCB99BA),
                'E' => XLColor.FromArgb(0x5A7881),
                'H' => XLColor.FromArgb(0xFADA9D),
                'I' => XLColor.FromArgb(0xD6DFB8),
                'L' => XLColor.FromArgb(0xB6D6E1),
                'P' => XLColor.FromArgb(0xE8F0DA),
                'T' => XLColor.FromArgb(0x7FD9E5),
                _ => XLColor.White,
            };

            columnA.Cell(cell.Address.RowNumber).Style.Fill.BackgroundColor = cell.Style
                .Fill
                .BackgroundColor;
        }
    }
}

public record ActionExportModel(
    int Id,
    string Number,
    string Name,
    string LeadDepartment,
    string Branch,
    string Objective,
    string DirectorsCommittee1,
    string DirectorsCommittee2,
    DateOnly? TargetCompletionDate,
    DateOnly? ActualCompletionDate,
    string InternalStatus,
    string InternalStatusUpdatedBy,
    DateTimeOffset? InternalStatusUpdatedDate,
    string ExternalStatus,
    string ExternalStatusUpdatedBy,
    DateTimeOffset? ExternalStatusUpdatedDate,
    string UndertakenInTheTraditionalTerritoriesOf,
    string EngagementsAndPartnershipActivities,
    string PublicExplanation,
    string AdditionalInternalInfo,
    string QuarterlyOrBiannualIndicators,
    string AnnualIndicators,
    string EditLink
);

public class XlsxExport
{
    public string FileName { get; }
    public string MimeType { get; } =
        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    public string FileExtension { get; } = "xlsx";
    public MemoryStream MemoryStream { get; } = new MemoryStream();

    public XlsxExport(string fileName)
    {
        FileName = $"{fileName}.{FileExtension}";
    }

    internal void Save(XLWorkbook workbook)
    {
        workbook.SaveAs(MemoryStream);
        MemoryStream.Seek(0, SeekOrigin.Begin);
    }
}

namespace OurCleanFuture.App.Services;

public class FileLoggerService : IFileLoggerService
{
    private readonly string _filePath;

    public FileLoggerService(string filePath)
    {
        try
        {
            FileInfo fi = new FileInfo(filePath);
            if (!Directory.Exists(fi.DirectoryName))
            {
                Directory.CreateDirectory(fi.DirectoryName);
            }

        }
        catch (Exception ex)
        {
        }

        _filePath = filePath;
    }

    public void Log(string message)
    {
        // Write log message to file
        try
        {
            File.AppendAllText(_filePath, $"{DateTime.Now}: {message}\n");
            if (new FileInfo( _filePath).Length > 5000000 )
            {
                string bkupFile = Path.Combine(new FileInfo(_filePath).DirectoryName, $"LogBkup{DateTime.Now.ToString("yyyyMMddHH")}.txt");
                File.Copy(_filePath, bkupFile, true );
                File.WriteAllText(_filePath, $"Log backed up on {DateTime.Now}" );
            }
        }
        catch (Exception ex)
        {
        }
    }
}

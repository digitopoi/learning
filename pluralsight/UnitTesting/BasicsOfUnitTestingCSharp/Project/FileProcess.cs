

public bool FileExists(string fileName) 
{
    if (string.IsNullOrEmpty(fileName))
    {
        throw new ArgumentNullException("filename");
    }

    return File.Exists(fileName);
}
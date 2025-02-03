using System;

public class CsvReader
{
    private string _filePath;

    public CsvReader(string filePath)
    {
        _filePath = filePath;
    }

    public List<string[]> ReadCSV()
    {
        List<string[]> data = new List<string[]>();

        using (StreamReader reader = new StreamReader(_filePath))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] values = line.Split(','); // 쉼표(,) 기준으로 분리
                data.Add(values);
            }
        }
        return data;
    }
}

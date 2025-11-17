using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class NaiveBayesEmail
{
    private Dictionary<string, int> spamCounts = new Dictionary<string, int>();
    private Dictionary<string, int> notspamCounts = new Dictionary<string, int>();
    private int spamWordTotal = 0;
    private int notspamWordTotal = 0;
    private int spamDocs = 0;
    private int notspamDocs = 0;

    private HashSet<string> vocabulary = new HashSet<string>();
    private List<string> Tokenize(string text)
    {
        char[] separators = { ' ', '\n', '\r', '\t', '.', ',', '!', '?', ':', ';', '-', '_', '/', '\\', '"', '\'' };
        return text
            .ToLower()
            .Split(separators, StringSplitOptions.RemoveEmptyEntries)
            .ToList();
    }
    public void Train(string folderSpam, string folderNotSpam)
    {
   
        foreach (string file in Directory.GetFiles(folderSpam, "*.txt"))
        {
            spamDocs++;
            string text = File.ReadAllText(file);
            var words = Tokenize(text);
            foreach (string word in words)
            {
                if (!spamCounts.ContainsKey(word))
                    spamCounts[word] = 0;
                spamCounts[word]++;
                spamWordTotal++;
                vocabulary.Add(word);
            }}
        foreach (string file in Directory.GetFiles(folderNotSpam, "*.txt"))
        {
            notspamDocs++;
            string text = File.ReadAllText(file);
            var words = Tokenize(text);

            foreach (string word in words)
            {
                if (!notspamCounts.ContainsKey(word))
                    notspamCounts[word] = 0;
                notspamCounts[word]++;
                notspamWordTotal++;
                vocabulary.Add(word);
            }}
        Console.WriteLine("Training complete.");
        Console.WriteLine($"Spam docs: {spamDocs}");
        Console.WriteLine($"NotSpam docs: {notspamDocs}");
        Console.WriteLine($"Vocabulary size: {vocabulary.Count}");
    }
    private double PWordGivenClass(string word, bool isSpam)
    {
        int count = isSpam
            ? (spamCounts.ContainsKey(word) ? spamCounts[word] : 0)
            : (notspamCounts.ContainsKey(word) ? notspamCounts[word] : 0);

        int total = isSpam ? spamWordTotal : notspamWordTotal;
        int V = vocabulary.Count;
        return (count + 1.0) / (total + V);
    }

    public string Classify(string documentText)
    {
        var words = Tokenize(documentText);
        double priorSpam = (double)spamDocs / (spamDocs + notspamDocs);
        double priorNotSpam = (double)notspamDocs / (spamDocs + notspamDocs);
        double logSpam = Math.Log(priorSpam);
        double logNotSpam = Math.Log(priorNotSpam);
        Console.WriteLine("\n--- CLASSIFICATION LOG ---");
        foreach (string word in words)
        {
            double pSpam = PWordGivenClass(word, true);
            double pNotSpam = PWordGivenClass(word, false);

            logSpam += Math.Log(pSpam);
            logNotSpam += Math.Log(pNotSpam);

            Console.WriteLine($"Word '{word}' => P(w|spam)={pSpam:F6}, P(w|notspam)={pNotSpam:F6}");
        }
        Console.WriteLine($"\nLog P(spam | D): {logSpam}");
        Console.WriteLine($"Log P(notspam | D): {logNotSpam}");

        if (logSpam > logNotSpam)
            return "SPAM";
        else
            return "NOT SPAM";
    }
}

class Program
{
    static void Main(string[] args)
    {
        var nb = new NaiveBayesEmail();
        string spamFolder = "spam";
        string notspamFolder = "notspam";
        nb.Train(spamFolder, notspamFolder);
        string testEmail = @"Congratulations! You won a free iPhone. Click now.";
        string result = nb.Classify(testEmail);
        Console.WriteLine($"\nFINAL CLASSIFICATION: {result}");
    }
}

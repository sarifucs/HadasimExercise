using System;
using System.Drawing;
using System.Net;

class Program
{
    static void Main()
    {
        // "https://raw.githubusercontent.com/kzjeef/algs4/master/burrows-wheelers/testfile/dickens.txt"
        // "C:/Users/mby/Documents/a.txt"

        try
        {
            Console.WriteLine("Please enter a path of any file"); //Get path of any file from the console
            string filePath = Console.ReadLine();

            string allFileText = ""; //Save all the text from file
            string[] allFileLines = { }; //Save all the text by lines
            List<string> allFileWords = new List<string>(); //Save all the text by words

            if (filePath == null || filePath == "") // Error receiving path of file
                Console.WriteLine("No file path received");
            else
            {
                if (filePath.Contains("http")) //The path is from the web
                    try
                    {
                        WebClient client = new WebClient();
                        Stream stream = client.OpenRead(filePath);
                        StreamReader reader = new StreamReader(stream);
                        allFileText = reader.ReadToEnd();
                        allFileLines = allFileText.Split("\n");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error reading file from web!!!!!!!");
                        Console.WriteLine(e);
                        return;
                    }
                else //The path is from the local computer
                {
                    try
                    {
                        allFileText = File.ReadAllText(filePath);
                        allFileLines = File.ReadAllLines(filePath);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error reading file from the local computer!!!!!!!");
                        Console.WriteLine(e);
                        return;
                    }
                }

                ///////////////////////////////////
                //11111111
                // The sum of the lines in the file
                Console.WriteLine("\nThe sum of lines in the file is " + allFileLines.Length + ".");

                ///////////////////////////////////
                //222222
                // The sum of the words in the file
                foreach (string line in allFileLines)
                {
                    string[] split_line = line.Split(' ');
                    foreach (string word in split_line) //Save all the words in allFileWords
                    {
                        string newWord = word.Replace(".", "").Replace(",", "").Replace(":", ""); //Delete the . , : and the like 
                        allFileWords.Add(newWord);
                    }

                }
                Console.WriteLine("\nThe sum of words in the file is " + allFileWords.Count() + ".");

                ///////////////////////////////////
                //33333
                // The sum of the unique words in the file
                Dictionary<string, int> theAllFileWordsDictionary = new Dictionary<string, int>(); //Save all the words with number of shows
                int uniqueCount = 0;
                foreach (string word in allFileWords)
                {
                    string lowerWord = word.ToLower();
                    if (theAllFileWordsDictionary.ContainsKey(lowerWord))  // Check - if word is already in dictionary update the count 
                    {
                        int value = theAllFileWordsDictionary[lowerWord];
                        theAllFileWordsDictionary[lowerWord] = value + 1; //sum of the word + 1
                    }
                    else  // If we found the same word we just increase the count in the dictionary 
                        theAllFileWordsDictionary.Add(lowerWord, 1);
                }
                foreach (KeyValuePair<string, int> word in theAllFileWordsDictionary) // Check how many unique words there are
                {
                    if (word.Value == 1)
                        uniqueCount++;
                }
                Console.WriteLine("\nThe file is contains " + uniqueCount + " unique words.");

                ///////////////////////////////////
                //44444
                // The length of the average sentence and the length of the maximum sentence
                string[] sentences = allFileText.Split('.'); //Each sentence ends in .
                int[] sentencesLength = new int[sentences.Length];  //Save all the length of sentences in the fie
                for (int i = 0; i < sentences.Length; i++)
                {
                    sentences[i] = sentences[i].Replace("\n", "").Replace("\r", ""); // The length without \n and without \r
                    sentencesLength[i] = sentences[i].Length;
                }
                Array.Sort(sentencesLength);
                Console.WriteLine("\nThe length of the average sentence is " + sentencesLength[sentencesLength.Length / 2 - 1] +
                ", and the length of the maximum sentence is " + sentencesLength[sentencesLength.Length - 1] + ".");

                ///////////////////////////////////
                //66666
                // The longest word sequence without 'k'
                List<string> largeSentenceWithoutK = new List<string>();
                List<string> currentSentence = new List<string>();
                foreach (string word in allFileWords)
                {
                    if (!word.Contains("k") && !word.Contains("K")) // Check if the word is not with k or K
                        currentSentence.Add(word);
                    else
                    {
                        if (currentSentence.Count() > largeSentenceWithoutK.Count()) // Check if the current sentence is the longest
                        {
                            largeSentenceWithoutK.Clear();
                            largeSentenceWithoutK.AddRange(currentSentence);
                        }
                        currentSentence.Clear(); // If replace the largeSentenceWithoutK and if the word contain k
                    }
                    if (currentSentence.Count() > largeSentenceWithoutK.Count()) // On the last sentence
                    {
                        largeSentenceWithoutK.Clear();
                        largeSentenceWithoutK.AddRange(currentSentence);
                    }
                }
                Console.WriteLine("\nThe longest word sequence without 'k' is:");
                foreach (string word in largeSentenceWithoutK)
                    Console.Write(word + " ");

                ///////////////////////////////////
                //88888
                // The colors names that appear in the file and and how many times each one appears
                KnownColor[] colors = (KnownColor[])Enum.GetValues(typeof(KnownColor)); // Save a list with colors names
                List<string> colorsString = new List<string>();
                foreach (KnownColor knowColor in colors)
                {
                    string color = Color.FromKnownColor(knowColor).Name;
                    color = color.ToLower();
                    colorsString.Add(color);
                }
                Dictionary<string, int> theAllColorsDictionary = new Dictionary<string, int>(); //Save all the names of colors in the file with number of shows
                foreach (string word in allFileWords)
                {
                    string lowerWord = word.ToLower();
                    if (colorsString.Contains(lowerWord)) // Check if the wors is color name
                        if (theAllColorsDictionary.ContainsKey(lowerWord)) // Check - if the color is already in dictionary update the count
                        {
                            int value = theAllColorsDictionary[lowerWord];
                            theAllColorsDictionary[lowerWord] = value + 1; //sum of the word + 1
                        }
                        else  // If we found the same color we just increase the count in the dictionary 
                            theAllColorsDictionary.Add(lowerWord, 1);
                }
                if (theAllColorsDictionary.Count > 0) //Check if the file contain colors names
                {
                    Console.WriteLine("\n\nThe colors names that appear in the file:");
                    foreach (KeyValuePair<string, int> colorWord in theAllColorsDictionary)
                    {
                        Console.WriteLine(colorWord.Key + " " + colorWord.Value + " times.");
                    }
                }
                else
                {
                    Console.WriteLine("\nThe file doesn't contain colors names.");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return;
        }
    }
}

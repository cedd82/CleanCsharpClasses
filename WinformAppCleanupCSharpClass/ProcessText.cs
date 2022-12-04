using System.Text.RegularExpressions;

namespace WinformAppCleanupCSharpClass;

public static class ProcessText
{
    public static string Process(string text)
    {
        return "";
    }

    public static void ProcessTwo()
    {
        var dir = @"C:\devMe\cyberSourceModelsForReqRes\Model1";
        var di = new DirectoryInfo(dir);


        var files2 = di.GetFiles();
//var files = Directory.GetFiles(dir);

        var outputDir = $"{di.FullName}out{DateTime.Now.Ticks}";
        var outDirectoryInfo = Directory.CreateDirectory(outputDir);
        foreach (var file in files2)
        {
            var fileInfo = file;
            var allText = File.ReadAllText(fileInfo.FullName);
            List<string> outputLines = new();

            var className = fileInfo.Name.Replace(".cs", "");
            var commentRegex = new Regex(@"^\s*/{1,2}.*$");


            var regexCtorString = @"^\s*public " + className + @".[\s*.*\{\w=\(\),;}]*";
            var removeCtorRegex = new Regex(regexCtorString, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var isMatch = false;
            isMatch = removeCtorRegex.IsMatch(allText);
            allText = removeCtorRegex.Replace(allText, "");


            //remove tostring
            //^\s*public override string ToString[\r\n\s*.*\{\w=\(\),;"\\:}]*
            var removeToString = @"^\s*public override string ToString[\r\n\s*.*\{\w=\(\),;""\\:}]*";
            var removeToStringRegex = new Regex(removeToString, RegexOptions.IgnoreCase | RegexOptions.Multiline);

            isMatch = removeToStringRegex.IsMatch(allText);
            allText = removeToStringRegex.Replace(allText, "");

            //tojson
            //^\s*public string ToJson[\r\n\s*.*\{\w=\(\),;"\\:}]*
            var removeToJson = @"^\s*public string ToJson[\r\n\s*.*\{\w=\(\),;""\\:}]*";
            var removeToJsonRegex = new Regex(removeToJson, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            isMatch = removeToJsonRegex.IsMatch(allText);
            allText = removeToJsonRegex.Replace(allText, "");

            //public override bool Equals
            //^\s*public override bool Equals[\r\n\s*.*\{\w=\(\),;"\\:/\}]*
            //var removeEquals = @"^\s*public override bool Equals[\r\n\s*.*\{\w=\(\),;"\\:/\}]*
            var removeEquals = @"^\s*public override bool Equals[\r\n\s*.*\{\w=\(\),;""\\:/\}]*";
            var removeEqualsRegex = new Regex(removeEquals, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            isMatch = removeEqualsRegex.IsMatch(allText);
            allText = removeEqualsRegex.Replace(allText, "");
            //removes public bool Equals(AccessTokenResponse other)
            //^\s*public bool Equals[\r\n\s*.*\{\w=\(\),;"\\:/\}\|!&]*
            var removeOtherEquals = @"^\s*public bool Equals[\r\n\s*.*\{\w=\(\),;""\\:/\}\|!&]*";
            var removeOtherEqualsRegex = new Regex(removeOtherEquals, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            isMatch = removeOtherEqualsRegex.IsMatch(allText);
            allText = removeOtherEqualsRegex.Replace(allText, "");

            //public override int GetHashCode()
            //^\s*public override int GetHashCode[\r\n\s*.*\{\w=\(\),;"\\:/\}\|!&\+]*
            var removeHash = @"^\s*public override int GetHashCode[\r\n\s*.*\{\w=\(\),;""\\:/\}\|!&\+]*";
            var removeHashRegex = new Regex(removeHash, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            isMatch = removeHashRegex.IsMatch(allText);
            allText = removeHashRegex.Replace(allText, "");

            //no ienumerable
            //^\s*IEnumerable<[\r\n\s*.*\{\w=\(\),;"\\:/\}\|!&\+>]*
            var removeIEnum = @"^\s*IEnumerable<[\r\n\s*.*\{\w=\(\),;""\\:/\}\|!&\+>]*";
            var removeIEnumRegex = new Regex(removeIEnum, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            isMatch = removeIEnumRegex.IsMatch(allText);
            allText = removeIEnumRegex.Replace(allText, "");

            var removeHeaderComment = @"^/[\*\r\n\s\w\.\/:-]*\n";
            var removeHeaderCommentRegex = new Regex(removeIEnum, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            isMatch = removeHeaderCommentRegex.IsMatch(allText);
            allText = removeHeaderCommentRegex.Replace(allText, "");


            var outFileName = fileInfo.Name;
            var outFileNameTemp = fileInfo.Name + "tmp";

            File.WriteAllText(outFileNameTemp, allText);

            var allLines = File.ReadAllLines(outFileNameTemp);
            foreach (var l in allLines)
            {
                if (l.StartsWith("using")) continue;
                if (commentRegex.IsMatch(l)) continue;
                if (l.StartsWith("namespace")) continue;
                if (l.StartsWith("<summary>")) continue;
                if (l.Contains($"{className} :"))
                {
                    var newLine = $"public class {className}";
                    outputLines.Add(newLine);
                }
                else
                {
                    outputLines.Add(l);
                }
            }

            outputLines.Add("}}");
            var outputFileNameFullPath = Path.Combine(outDirectoryInfo.FullName, outFileName);
            File.WriteAllLines(outputFileNameFullPath, outputLines);

            File.Delete(outFileNameTemp);
        }
    }
}
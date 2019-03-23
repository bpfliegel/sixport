using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Sixport.CodeGenerator
{
    class Program
    {
        static Dictionary<string, string> templates = new Dictionary<string, string>();
        static string eol = "\r\n\r\n";

        static string LoadFile(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            string content = sr.ReadToEnd();
            sr.Close();
            return content;
        }

        static void LoadTemplates()
        {
            string dir = @"..\..\Templates\";
            foreach (string filename in Directory.GetFiles(dir, "*.txt"))
            {
                string content = LoadFile(filename);
                string shortname = filename.Replace(dir, "").Replace(".txt", "");
                templates.Add(shortname, content);
            }
        }

        static void Main(string[] args)
        {
            LoadTemplates();

            string algo = LoadFile(@"..\..\algo.txt");
            string[] split = algo.Replace("\r", "").Split('\n');

            Dictionary<int, List<string>> algos = new Dictionary<int, List<string>>();
            int index = -1;
            List<string> lines = new List<string>();
            for (int i = 0; i < split.Length - 1; i++)
            {
                string line = split[i];
                if (index == -1)
                {
                    index = int.Parse(line.Trim());
                    lines = new List<string>();
                }
                else if (line.Length == 0)
                {
                    algos.Add(index, lines);
                    index = -1;
                }
                else
                {
                    lines.Add(line.Trim());
                }
            }

            // Create also the operator increment code
            StringBuilder op_inc = new StringBuilder();
            for (int i = 6; i > 0; i--)
            {
                op_inc.Append(templates["op"].Replace("%X%", i.ToString()));
            }

            StringBuilder subs = new StringBuilder();
            StringBuilder algs = new StringBuilder();
            StringBuilder subs2 = new StringBuilder();
            StringBuilder algs2 = new StringBuilder();
            foreach (int key in algos.Keys)
            {
                subs.Append(templates["switch_sub"].Replace("%X%", key.ToString())).Append(eol);
                algs.Append(templates["alg"].Replace("%OP_INC%", op_inc.ToString()).Replace("%X%", key.ToString()).Replace("%OUTPUT%", ProcessAlgo(key, algos[key]))).Append(eol);
                subs2.Append(templates["switch_sub2"].Replace("%X%", key.ToString())).Append(eol);
                algs2.Append(templates["alg2"].Replace("%OP_INC%", op_inc.ToString()).Replace("%X%", key.ToString()).Replace("%OUTPUT%", ProcessAlgo(key, algos[key]))).Append(eol);
            }

            string final = templates["switch_main"].Replace("%SUBS1%", subs.ToString()).Replace("%ALGS1%", algs.ToString()).Replace("%SUBS2%", subs2.ToString()).Replace("%ALGS2%", algs2.ToString());

            StreamWriter sw = new StreamWriter(@"..\..\..\Sixport\dx7_voice.render_auto.cs");
            sw.Write(final);
            sw.Close();
            sw.Dispose();
        }

        // c = car, k = car_sfb
        // m = mod, n = mod_sfb

        // f - + this.feedback
        // i - + <supplied by definition> 
        // 0 - <nothing>

        // c1(m4(m5(m60))+n3f+m20)

        // def n3f
        // c4(m60+m50)
        // c2i
        // c1i

        // c3(m5(m60)+n4f)
        // c1(m20)
        static string def = "";

        private static string ProcessAlgo(int key, List<string> list)
        {
            StringBuilder sb = new StringBuilder();

            string output = "";
            foreach (string line in list)
            {
                if (line.StartsWith("def"))
                {
                    def = ProcessToken(ref sb, line.Substring(4));
                }
                else
                {
                    ProcessToken(ref sb, line); // line
                    output += "+ " + "final_" + line.Substring(1, 1);
                }
            }

            return sb.ToString() + string.Format("output = ({0});", output.Substring(2));
        }

        private static string ProcessToken(ref StringBuilder sb, string token)
        {
            StringBuilder response = new StringBuilder();
            while (token.Length > 0)
            {
                string templateID = token.Substring(0, 1);
                if (templateID == "c") { templateID = "car"; }
                if (templateID == "k") { templateID = "car_sfb"; }
                if (templateID == "m") { templateID = "mod"; }
                if (templateID == "n") { templateID = "mod_sfb"; }
                if (templateID == "+")
                {
                    token = token.Substring(1);
                    response.Append("+ ");
                    continue;
                }
                string template = templates[templateID];

                int opIndex = int.Parse(token.Substring(1, 1));

                string phaseOp = token.Substring(2, 1);
                if (phaseOp == "0")
                {
                    // Terminal node, can be generated
                    sb.Append(template.Replace("%X%", opIndex.ToString()).Replace("%_p%", "")).Append(eol);
                    response.Append("+ final_" + opIndex.ToString());
                    token = token.Substring(3);
                }
                if (phaseOp == "f")
                {
                    // Terminal node

                    // Generate and return
                    sb.Append(template.Replace("%X%", opIndex.ToString()).Replace("%_p%", "+ this.feedback")).Append(eol);
                    response.Append("+ final_" + opIndex.ToString());
                    token = token.Substring(3);
                }
                if (phaseOp == "i")
                {
                    // Terminal node

                    // Generate and return
                    sb.Append(template.Replace("%X%", opIndex.ToString()).Replace("%_p%", "+ " + def)).Append(eol);
                    response.Append("+ final_" + opIndex.ToString());
                    token = token.Substring(3);
                }
                if (phaseOp == "(")
                {
                    // Find closing index
                    int brIndex = 1;
                    bool go = true;
                    int idx = 3;
                    while (go)
                    {
                        string z = token.Substring(idx, 1);
                        if (z == "(") { brIndex++; }
                        if (z == ")") { brIndex--; }
                        if (brIndex == 0) { go = false; }
                        else idx++;
                    }

                    string internalToken = token.Substring(3, idx - 3);
                    string internalResponse = ProcessToken(ref sb, internalToken);
                    response.Append("+ final_" + opIndex.ToString());
                    sb.Append(template.Replace("%X%", opIndex.ToString()).Replace("%_p%", "+ " + internalResponse)).Append(eol);
                    token = token.Substring(idx + 1);
                }
            }

            return response.ToString().Substring(2);
        }
    }
}

using System.Text;

namespace SWLOR.Game.Server.Service.LanguageService
{
    public class TranslatorGungan : ITranslator
    {
        public string Translate(string message)
        {
            var sb = new StringBuilder();
            foreach (var ch in message)
            {
                switch (ch)
                {
                    case 'a': sb.Append("i"); break;
                    case 'A': sb.Append("I"); break;
                    case 'b': sb.Append("j"); break;
                    case 'B': sb.Append("J"); break;
                    case 'c': sb.Append("k"); break;
                    case 'C': sb.Append("K"); break;
                    case 'd': sb.Append("l"); break;
                    case 'D': sb.Append("L"); break;
                    case 'e': sb.Append("m"); break;
                    case 'E': sb.Append("M"); break;
                    case 'f': sb.Append("n"); break;
                    case 'F': sb.Append("N"); break;
                    case 'g': sb.Append("o"); break;
                    case 'G': sb.Append("O"); break;
                    case 'h': sb.Append("p"); break;
                    case 'H': sb.Append("P"); break;
                    case 'i': sb.Append("kw"); break;
                    case 'I': sb.Append("Kw"); break;
                    case 'j': sb.Append("r"); break;
                    case 'J': sb.Append("R"); break;
                    case 'k': sb.Append("s"); break;
                    case 'K': sb.Append("S"); break;
                    case 'l': sb.Append("t"); break;
                    case 'L': sb.Append("T"); break;
                    case 'm': sb.Append("u"); break;
                    case 'M': sb.Append("U"); break;
                    case 'n': sb.Append("v"); break;
                    case 'N': sb.Append("V"); break;
                    case 'o': sb.Append("w"); break;
                    case 'O': sb.Append("W"); break;
                    case 'p': sb.Append("ks"); break;
                    case 'P': sb.Append("Ks"); break;
                    case 'q': sb.Append("y"); break;
                    case 'Q': sb.Append("Y"); break;
                    case 'r': sb.Append("z"); break;
                    case 'R': sb.Append("Z"); break;
                    case 's': sb.Append("a"); break;
                    case 'S': sb.Append("A"); break;
                    case 't': sb.Append("b"); break;
                    case 'T': sb.Append("B"); break;
                    case 'u': sb.Append("ch"); break;
                    case 'U': sb.Append("Ch"); break;
                    case 'v': sb.Append("d"); break;
                    case 'V': sb.Append("D"); break;
                    case 'w': sb.Append("e"); break;
                    case 'W': sb.Append("E"); break;
                    case 'x': sb.Append("f"); break;
                    case 'X': sb.Append("F"); break;
                    case 'y': sb.Append("g"); break;
                    case 'Y': sb.Append("G"); break;
                    case 'z': sb.Append("h"); break;
                    case 'Z': sb.Append("H"); break;
                    default: sb.Append(ch); break;
                }
            }
            return sb.ToString();
        }
    }
}

using System;
using System.Text;

namespace System
{
    public static class ExtensionMethods
    {
        public static string EncodeBase64(this string value)
        {
            var valueBytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(valueBytes);
        }

        public static string DecodeBase64(this string value)
        {
            var valueBytes = Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(valueBytes);
        }

        public static string ToStringTextoCentralizado(this String Texto, int TamanhoTotalDoCampo)
        {
            if (TamanhoTotalDoCampo == 0)
                return string.Empty;

            if (!string.IsNullOrWhiteSpace(Texto))
            {
                Texto = Texto.Trim();

                if (Texto.Length > TamanhoTotalDoCampo)
                    return Texto.Substring(TamanhoTotalDoCampo);

                if (Texto.Length == TamanhoTotalDoCampo || Texto.Length == TamanhoTotalDoCampo - 1)
                    return Texto;

                int metade = (TamanhoTotalDoCampo - Texto.Length) / 2;

                return "".PadLeft(metade, ' ') + Texto;
            }
            else
                return string.Empty;
        }

        public static string ToStrPortugues(this DayOfWeek valor)
        {
            switch (valor)
            {
                case DayOfWeek.Sunday:
                    return "Domingo";
                    break;
                case DayOfWeek.Monday:
                    return "Segunda-Feira";
                    break;
                case DayOfWeek.Tuesday:
                    return "Terça-Feira";
                    break;
                case DayOfWeek.Wednesday:
                    return "Quarta-Feira";
                    break;
                case DayOfWeek.Thursday:
                    return "Quinta-Feira";
                    break;
                case DayOfWeek.Friday:
                    return "Sexta-Feira";
                    break;
                case DayOfWeek.Saturday:
                    return "Sábado";
                    break;
                default:
                    return string.Empty;
            }
        }

        public static int ToInt32(this String valor)
        {
            try
            {
                int.TryParse(valor, out var output);
                return output;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public static double ToDouble(this String valor)
        {
            double output;
            double.TryParse(valor, out output);
            return output;
        }

        public static async Task<byte[]> ToByteArray(this Stream stream)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    await stream.CopyToAsync(ms);
                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static double? ToDoubleNull(this String valor)
        {
            double output;
            var convertido = double.TryParse(valor, out output);
            return convertido ? output : (double?)null;
        }

        public static DateTime ToDateTime(this String valor)
        {
            DateTime output;
            DateTime.TryParse(valor, out output);
            return output;
        }

        public static decimal ToDecimal(this String valor)
        {
            decimal output;
            try
            {
                decimal.TryParse(valor, out output);
            }
            catch (Exception ex)
            {
                return 0;
            }

            return output;
        }

        public static decimal ToDecimalArredondando(this Decimal valor, int casasDecimais)
        {
            var output = Math.Round(valor, casasDecimais);
            return output;
        }

        public static decimal ToDecimalCapado(this Decimal valor, int casasDecimais)
        {
            if (valor.ToString().Split(',').Length == 2)
            {
                var outPut = valor.ToString().Split(',')[1].Substring(0, valor.ToString().Split(',')[1].Length);
                if (outPut.Length > casasDecimais)
                {
                    valor *= ("1" + "".PadRight(casasDecimais, "0"[0])).ToInt32();
                    valor = Math.Truncate(valor);
                    valor /= ("1" + "".PadRight(casasDecimais, "0"[0])).ToInt32();
                }
            }

            return valor;
        }

        public static DateTime ToDateTimeComTime000000(this DateTime valor)
        {
            return new DateTime(valor.Year, valor.Month, valor.Day);
        }

        public static DateTime ToDateTimeComTime235959(this DateTime valor)
        {
            return new DateTime(valor.Year, valor.Month, valor.Day, 23, 59, 59);
        }

        public static DateTime ToDateTimeComTimeHH0000(this DateTime valor, int hora)
        {
            return new DateTime(valor.Year, valor.Month, valor.Day, hora, 0, 0);
        }

        public static DateTime ToDateTimeComTimeHHMMSS(this DateTime valor, int hora, int minuto, int segundo)
        {
            return new DateTime(valor.Year, valor.Month, valor.Day, hora, minuto, segundo);
        }

        public static DateTime ToDateTimeInicioMesComTimeHHMMSS(this DateTime dateTime, int hora, int minuto, int segundo)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1, hora, minuto, segundo);
        }

        public static DateTime ToDateTimeFinalMesComTimeHHMMSS(this DateTime dateTime, int hora, int minuto, int segundo)
        {
            return new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month), hora, minuto, segundo);
        }

        public static long ToLong(this String valor)
        {
            long output;
            long.TryParse(valor, out output);
            return output;
        }

        public static bool ToBool(this String valor)
        {
            bool output;
            bool.TryParse(valor, out output);
            return output;
        }
    }
}



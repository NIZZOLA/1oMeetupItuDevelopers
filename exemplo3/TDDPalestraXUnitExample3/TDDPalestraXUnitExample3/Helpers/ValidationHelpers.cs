using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TDDPalestraXUnitExample3.Helpers
{
    public static class ValidationHelpers
    {
        public static bool ValidateZip(string zip)
        {
            return Regex.IsMatch(zip, @"^[0-9]{2}.[0-9]{3}-[0-9]{3}$");
        }

        public static bool ValidateEmail(string email)
        {
            return Regex.IsMatch(email, @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        }

        public static bool ValidatePhone(string phone)
        {
            return Regex.IsMatch(phone, @"^\([1-9]{2}\) [2-9][0-9]{4}\-[0-9]{4}$");
        }

        public static bool ValidateCNPJ(string cnpjString)
        {
            if (!Regex.IsMatch(cnpjString, @"^\d{2}\.\d{3}\.\d{3}\/\d{4}\-\d{2}$"))
                return false;

            return IsCNPJ(cnpjString);

        }

        public static bool ValidateCPF(string cpfString)
        {
            if (!Regex.IsMatch(cpfString, @"^\d{3}\.\d{3}\.\d{3}\-\d{2}$"))
                return false;

            return IsCPF(cpfString);
        }

        public static bool IsCNPJ(string cnpj)
        {
            int i, plus;

            char[] _cnpj = new char[14];

            char[] mult = new char[13] { '6', '5', '4', '3', '2', '9', '8', '7', '6', '5', '4', '3', '2' };

            plus = 0;

            cnpj = cnpj.Replace(".", "");

            cnpj = cnpj.Replace("/", "");

            cnpj = cnpj.Replace("-", "");

            if (cnpj.Length < 14) { return false; }

            for (i = 0; i < 12; i++)
            {
                _cnpj[i] = cnpj[i];

                plus += Convert.ToInt32(_cnpj[i].ToString()) * Convert.ToInt32(mult[i + 1].ToString());
            }

            if ((i = plus % 11) < 2) { _cnpj[12] = '0'; } else { _cnpj[12] = Convert.ToChar((11 - i).ToString()); }

            plus = 0;

            for (i = 0; i < 13; i++)
            {
                plus += (Convert.ToInt32(_cnpj[i].ToString()) * Convert.ToInt32(mult[i].ToString()));
            }

            if ((i = plus % 11) < 2) { _cnpj[13] = '0'; } else { _cnpj[13] = Convert.ToChar(Convert.ToString(11 - i)); }

            if (cnpj[12] != _cnpj[12] || cnpj[13] != _cnpj[13]) { return false; }

            return true;
        }

        public static bool IsRG(string rg)
        {
            return !string.IsNullOrEmpty(rg) && rg.Length < 18;
        }

        public static bool IsCPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public static bool IsGuid(string guid)
        {
            return Guid.TryParse(guid, out Guid x);
        }

        public static bool IsDate(string date)
        {
            return DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.GetCultureInfo("pt-BR"), DateTimeStyles.None, out DateTime dateResult);
        }

        /// <summary>
        /// Verificar se o Enum é valido. 
        /// 1) Objeto injetado deve ser do tipo Enum;
        /// 2) Deve ser possível converter o paramentro, seja texto ou número, para um atribvuto válido do enum;
        /// 3) Ao colocar um número inválido, o TryParse ignora e gera um falso positivo, então é verificado se o resultado é dígido.
        /// -> Se o resultado é um digito, então é inválido pois o retorno do TryParse é o nome do atributo do Enum;
        /// </summary>
        /// <typeparam name="EnumType">Deve ser o Enum que deverá ser verificado</typeparam>
        /// <param name="_enum">testo ou número do atributo do Enum</param>
        /// <returns></returns>
        public static bool IsValidEnum<EnumType>(string _enum)
        {
            if (!typeof(EnumType).IsEnum || !Enum.TryParse(typeof(EnumType), _enum, out object valueResult) || valueResult.ToString().All(c => char.IsDigit(c)))
                return false;
            return true;
        }

    }
}

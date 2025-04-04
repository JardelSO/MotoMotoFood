using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MotoMotoFood.Services
{
    class SegundoFatorService
    {
        private static string remetente = "foodmotomoto@gmail.com";

        private static string senha = "wllj ybpj hnea rrpz";

        private static string assunto = "E-mail Confirmação";

        private static string corpo = "Segue o código de confirmação do email para o MOTO MOTO FOOD: ";

        public static void EnviarEmailConfirmacao(string emailDestino, int code)
        {
            try
            {
                MailMessage mensagem = new MailMessage(remetente, emailDestino, assunto, corpo + code);

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(remetente, senha);
                smtp.EnableSsl = true;

                smtp.Send(mensagem);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}");
                throw ex;
            }
        }

    }
}

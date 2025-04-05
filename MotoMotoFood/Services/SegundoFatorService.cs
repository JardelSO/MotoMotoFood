using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mime;

namespace MotoMotoFood.Services
{
    class SegundoFatorService
    {
        private static string remetente = "foodmotomoto@gmail.com";

        private static string senha = "wllj ybpj hnea rrpz";

        private static string assunto = "E-mail Confirmação";

        public static void EnviarEmailConfirmacao(string emailDestino, int code)
        {
            try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string projectPath = Directory.GetParent(basePath).Parent.Parent.Parent.FullName;
                string logoPath = Path.Combine(projectPath, "Util", "img", "logoMotoMotoFood.png");

                if (!File.Exists(logoPath))
                {
                    Console.WriteLine("Erro: A imagem do logo não foi encontrada no caminho especificado.");
                    return;
                }

                MailMessage mensagem = new MailMessage();
                mensagem.From = new MailAddress(remetente, "Moto Moto Food");
                mensagem.To.Add(emailDestino);
                mensagem.Subject = assunto;
                mensagem.IsBodyHtml = true;

                string corpoHtml = $@"
                <html>
                <body style='font-family: Arial, sans-serif; text-align: center;'>
                    <img src='cid:logoMotoMotoFood' width='200' style='margin-bottom: 10px;' />
                    <h2>Bem-vindo ao Moto Moto Food! 🍔🚴‍♂️</h2>
                    <p>Para garantir a segurança da sua conta, utilize o código abaixo para confirmar seu e-mail:</p>
                    <h1 style='color: #ff4500; font-size: 24px;'>{code}</h1>
                    <p>Seu pedido está quase pronto! Após a confirmação, você poderá aproveitar todas as delícias entregues pelo nosso hipopótamo mais rápido da cidade! 🦛💨</p>
                    <p><small>Se não solicitou este código, ignore este e-mail.</small></p>
                    <p><strong>Equipe Moto Moto Food</strong></p>
                </body>
                </html>";

                mensagem.Body = corpoHtml;

                // Anexando a imagem como um LinkedResource
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(corpoHtml, null, MediaTypeNames.Text.Html);
                LinkedResource logo = new LinkedResource(logoPath, MediaTypeNames.Image.Jpeg)
                {
                    ContentId = "logoMotoMotoFood",
                    TransferEncoding = TransferEncoding.Base64
                };
                htmlView.LinkedResources.Add(logo);
                mensagem.AlternateViews.Add(htmlView);

                // Configurar SMTP
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(remetente, senha);
                smtp.EnableSsl = true;

                // Enviar e-mail
                smtp.Send(mensagem);

                Console.WriteLine("E-mail enviado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}");
            }
        }
    }

    
}

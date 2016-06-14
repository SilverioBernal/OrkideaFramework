using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.Messaging
{
    public class Mailing
    {
        public string smtpServer { get; set; }
        public int smtpPort { get; set; }
        public bool smtpEnableSSL { get; set; }
        public string smtpUser { get; set; }
        public string smtpPassword { get; set; }

        #region Constructor
        public Mailing()
        {

        }

        public Mailing(string SMTPServer, int SMTPPort, bool SMTPEnableSSL, string SMTPUser, string SMTPPassword)
        {
            smtpServer = SMTPServer;
            smtpPort = SMTPPort;
            smtpEnableSSL = SMTPEnableSSL;
            smtpUser = SMTPUser;
            smtpPassword = SMTPPassword;
        }
        #endregion

        #region Plaintext mailing
        public void SendMail(MailAddress from, List<MailAddress> to, string subject, string body)
        {
            try
            {
                var smtp = getSmtpSettings();
                using (var message = new MailMessage())
                {
                    message.From = from;
                    foreach (MailAddress item in to)
                    {
                        message.To.Add(item);
                    }
                    message.Body = body;
                    message.Subject = subject;
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SendMail(MailAddress from, List<MailAddress> to, List<MailAddress> cc, string subject, string body)
        {
            try
            {
                var smtp = getSmtpSettings();
                using (var message = new MailMessage())
                {
                    message.From = from;
                    foreach (MailAddress item in to)
                        message.To.Add(item);

                    foreach (MailAddress item in cc)
                        message.CC.Add(item);

                    message.Body = body;
                    message.Subject = subject;
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SendMail(MailAddress from, List<MailAddress> to, List<MailAddress> cc, List<MailAddress> bcc, string subject, string body)
        {
            try
            {
                var smtp = getSmtpSettings();
                using (var message = new MailMessage())
                {
                    message.From = from;
                    foreach (MailAddress item in to)
                    {
                        message.To.Add(item);
                    }
                    foreach (MailAddress item in cc)
                    {
                        message.CC.Add(item);
                    }
                    foreach (MailAddress item in bcc)
                    {
                        message.Bcc.Add(item);
                    }
                    message.Body = body;
                    message.Subject = subject;
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void SendMail(MailAddress from, List<MailAddress> to, string subject, string body, Dictionary<string, string> dynamicValues)
        {
            try
            {
                var smtp = getSmtpSettings();
                using (var message = new MailMessage())
                {
                    message.From = from;
                    foreach (MailAddress item in to)
                        message.To.Add(item);

                    //Set dinamyc values
                    foreach (var item in dynamicValues)
                        body = body.Replace(item.Key, item.Value);

                    message.Body = body;
                    message.Subject = subject;
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SendMail(MailAddress from, List<MailAddress> to, List<MailAddress> cc, string subject, string body, Dictionary<string, string> dynamicValues)
        {
            try
            {
                var smtp = getSmtpSettings();
                using (var message = new MailMessage())
                {
                    message.From = from;
                    foreach (MailAddress item in to)
                    {
                        message.To.Add(item);
                    }
                    foreach (MailAddress item in cc)
                    {
                        message.CC.Add(item);
                    }

                    //Set dinamyc values
                    foreach (var item in dynamicValues)
                        body = body.Replace(item.Key, item.Value);

                    message.Body = body;
                    message.Subject = subject;
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SendMail(MailAddress from, List<MailAddress> to, List<MailAddress> cc, List<MailAddress> bcc, string subject, string body, Dictionary<string, string> dynamicValues)
        {
            try
            {
                var smtp = getSmtpSettings();
                using (var message = new MailMessage())
                {
                    message.From = from;
                    foreach (MailAddress item in to)
                        message.To.Add(item);

                    foreach (MailAddress item in cc)
                        message.CC.Add(item);

                    foreach (MailAddress item in bcc)
                        message.Bcc.Add(item);

                    //Set dinamyc values
                    foreach (var item in dynamicValues)
                        body = body.Replace(item.Key, item.Value);

                    message.Body = body;
                    message.Subject = subject;
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Html mailing
        public void SendMail(MailAddress from, List<MailAddress> to, string subject, string htmlText, string plainText)
        {
            try
            {
                var smtp = getSmtpSettings();

                // create the mail message
                MailMessage mail = new MailMessage();

                // set the addresses
                mail.From = from;
                foreach (MailAddress item in to)
                    mail.To.Add(item);

                // set the content
                mail.Subject = subject;
                AlternateView plainView = AlternateView.CreateAlternateViewFromString(plainText, null, "text/plain");

                // then we create the Html part to embed images, we need to use the prefix 'cid' in the img src value                
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlText, null, "text/html");

                // add the views
                mail.AlternateViews.Add(plainView);
                mail.AlternateViews.Add(htmlView);

                // send mail
                smtp.Send(mail);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SendMail(MailAddress from, List<MailAddress> to, List<MailAddress> cc, string subject, string htmlText, string plainText)
        {
            try
            {
                var smtp = getSmtpSettings();

                // create the mail message
                MailMessage mail = new MailMessage();

                // set the addresses
                mail.From = from;
                foreach (MailAddress item in to)
                    mail.To.Add(item);

                foreach (MailAddress item in cc)
                    mail.CC.Add(item);

                // set the content
                mail.Subject = subject;
                AlternateView plainView = AlternateView.CreateAlternateViewFromString(plainText, null, "text/plain");

                // then we create the Html part to embed images, we need to use the prefix 'cid' in the img src value                
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlText, null, "text/html");

                // add the views
                mail.AlternateViews.Add(plainView);
                mail.AlternateViews.Add(htmlView);

                // send mail
                smtp.Send(mail);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SendMail(MailAddress from, List<MailAddress> to, List<MailAddress> cc, List<MailAddress> bcc, string subject, string htmlText, string plainText)
        {
            try
            {
                var smtp = getSmtpSettings();

                // create the mail message
                MailMessage mail = new MailMessage();

                // set the addresses
                mail.From = from;
                foreach (MailAddress item in to)
                    mail.To.Add(item);

                foreach (MailAddress item in cc)
                    mail.CC.Add(item);

                // set the content
                mail.Subject = subject;
                AlternateView plainView = AlternateView.CreateAlternateViewFromString(plainText, null, "text/plain");

                // then we create the Html part to embed images, we need to use the prefix 'cid' in the img src value                
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlText, null, "text/html");

                // add the views
                mail.AlternateViews.Add(plainView);
                mail.AlternateViews.Add(htmlView);

                // send mail
                smtp.Send(mail);
            }
            catch (Exception)
            {
                throw;
            }
        }



        public void SendMail(MailAddress from, List<MailAddress> to, string subject, string htmlText, string plainText, List<LinkedResource> linkedResources)
        {
            try
            {
                SmtpClient smtp = getSmtpSettings();

                // create the mail message
                MailMessage mail = new MailMessage();

                // set the addresses
                mail.From = from;
                foreach (MailAddress item in to)
                    mail.To.Add(item);

                // set the content
                mail.Subject = subject;
                AlternateView plainView = AlternateView.CreateAlternateViewFromString(plainText, null, "text/plain");

                // then we create the Html part to embed images, we need to use the prefix 'cid' in the img src value                
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlText, null, "text/html");

                // create image resource from image path using LinkedResource class..
                foreach (LinkedResource item in linkedResources)
                    htmlView.LinkedResources.Add(item);


                // add the views
                mail.AlternateViews.Add(plainView);
                mail.AlternateViews.Add(htmlView);

                // send mail
                smtp.Send(mail);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SendMail(MailAddress from, List<MailAddress> to, List<MailAddress> cc, string subject, string htmlText, string plainText, List<LinkedResource> linkedResources)
        {
            try
            {
                var smtp = getSmtpSettings();

                // create the mail message
                MailMessage mail = new MailMessage();

                // set the addresses
                mail.From = from;
                foreach (MailAddress item in to)
                    mail.To.Add(item);

                foreach (MailAddress item in cc)
                    mail.CC.Add(item);

                // set the content
                mail.Subject = subject;
                AlternateView plainView = AlternateView.CreateAlternateViewFromString(plainText, null, "text/plain");

                // then we create the Html part to embed images, we need to use the prefix 'cid' in the img src value                
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlText, null, "text/html");

                // create image resource from image path using LinkedResource class..
                foreach (LinkedResource item in linkedResources)
                    htmlView.LinkedResources.Add(item);


                // add the views
                mail.AlternateViews.Add(plainView);
                mail.AlternateViews.Add(htmlView);

                // send mail
                smtp.Send(mail);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SendMail(MailAddress from, List<MailAddress> to, List<MailAddress> cc, List<MailAddress> bcc, string subject, string htmlText, string plainText, List<LinkedResource> linkedResources)
        {
            try
            {
                var smtp = getSmtpSettings();

                // create the mail message
                MailMessage mail = new MailMessage();

                // set the addresses
                mail.From = from;
                foreach (MailAddress item in to)
                    mail.To.Add(item);

                foreach (MailAddress item in cc)
                    mail.CC.Add(item);

                // set the content
                mail.Subject = subject;
                AlternateView plainView = AlternateView.CreateAlternateViewFromString(plainText, null, "text/plain");

                // then we create the Html part to embed images, we need to use the prefix 'cid' in the img src value                
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlText, null, "text/html");

                // create image resource from image path using LinkedResource class..
                foreach (LinkedResource item in linkedResources)
                    htmlView.LinkedResources.Add(item);


                // add the views
                mail.AlternateViews.Add(plainView);
                mail.AlternateViews.Add(htmlView);

                // send mail
                smtp.Send(mail);
            }
            catch (Exception)
            {
                throw;
            }
        }



        public void SendMail(MailAddress from, List<MailAddress> to, string subject, string htmlText, string plainText, List<LinkedResource> linkedResources, Dictionary<string, string> dynamicValues)
        {
            try
            {
                SmtpClient smtp = getSmtpSettings();

                // create the mail message
                MailMessage mail = new MailMessage();

                // set the addresses
                mail.From = from;
                foreach (MailAddress item in to)
                    mail.To.Add(item);

                // set the content
                mail.Subject = subject;

                //Set dinamyc values
                foreach (var item in dynamicValues)
                {
                    htmlText = htmlText.Replace(item.Key, item.Value);
                    plainText = plainText.Replace(item.Key, item.Value);
                }

                AlternateView plainView = AlternateView.CreateAlternateViewFromString(plainText, null, "text/plain");
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlText, null, "text/html");

                // adding the imaged linked to htmlView...
                foreach (LinkedResource item in linkedResources)
                    htmlView.LinkedResources.Add(item);

                // add the views
                mail.AlternateViews.Add(plainView);
                mail.AlternateViews.Add(htmlView);

                // send mail
                smtp.Send(mail);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SendMail(MailAddress from, List<MailAddress> to, List<MailAddress> cc, string subject, string htmlText, string plainText, List<LinkedResource> linkedResources, Dictionary<string, string> dynamicValues)
        {
            try
            {
                var smtp = getSmtpSettings();

                // create the mail message
                MailMessage mail = new MailMessage();

                // set the addresses
                mail.From = from;
                foreach (MailAddress item in to)
                    mail.To.Add(item);

                foreach (MailAddress item in cc)
                    mail.CC.Add(item);

                // set the content
                mail.Subject = subject;

                //Set dinamyc values
                foreach (var item in dynamicValues)
                {
                    htmlText = htmlText.Replace(item.Key, item.Value);
                    plainText = plainText.Replace(item.Key, item.Value);
                }

                AlternateView plainView = AlternateView.CreateAlternateViewFromString(plainText, null, "text/plain");
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlText, null, "text/html");

                // adding the imaged linked to htmlView...
                foreach (LinkedResource item in linkedResources)
                    htmlView.LinkedResources.Add(item);

                // add the views
                mail.AlternateViews.Add(plainView);
                mail.AlternateViews.Add(htmlView);

                // send mail
                smtp.Send(mail);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SendMail(MailAddress from, List<MailAddress> to, List<MailAddress> cc, List<MailAddress> bcc, string subject, string htmlText, string plainText, List<LinkedResource> linkedResources, Dictionary<string, string> dynamicValues)
        {
            try
            {
                var smtp = getSmtpSettings();

                // create the mail message
                MailMessage mail = new MailMessage();

                // set the addresses
                mail.From = from;
                foreach (MailAddress item in to)
                    mail.To.Add(item);

                foreach (MailAddress item in cc)
                    mail.CC.Add(item);

                foreach (MailAddress item in bcc)
                    mail.Bcc.Add(item);

                // set the content
                mail.Subject = subject;

                //Set dinamyc values
                foreach (var item in dynamicValues)
                {
                    htmlText = htmlText.Replace(item.Key, item.Value);
                    plainText = plainText.Replace(item.Key, item.Value);
                }

                AlternateView plainView = AlternateView.CreateAlternateViewFromString(plainText, null, "text/plain");
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlText, null, "text/html");

                // adding the imaged linked to htmlView...
                foreach (LinkedResource item in linkedResources)
                    htmlView.LinkedResources.Add(item);

                // add the views
                mail.AlternateViews.Add(plainView);
                mail.AlternateViews.Add(htmlView);

                // send mail
                smtp.Send(mail);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        private SmtpClient getSmtpSettings()
        {
            return new SmtpClient()
            {
                Host = smtpServer,
                Port = smtpPort,
                EnableSsl = smtpEnableSSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(smtpUser, smtpPassword)
            };
        }
    }
}

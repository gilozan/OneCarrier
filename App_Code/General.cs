using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using MimeKit;
using System.IO;

/// <summary>
/// Summary description for General
/// </summary>
public static class General
{

    public static string tasaGeneral = "0.160000";
    public static string sendEmailTo = "glozano@evoctus.com";
    public static string tenant = "infnit";

    public static string parseJSON(string str)
    {
        return str.Replace("\\", "\\\\").Replace("\"", "").Replace("'", "\'").Replace("{", "").Replace("}", "").Replace("\r", "").Replace("\n", "");
    }
    
    public static string sendEmail(string email, string subject, string msg)
    {
        try
        {
            Amazon.SimpleEmail.AmazonSimpleEmailServiceConfig config = new Amazon.SimpleEmail.AmazonSimpleEmailServiceConfig();
            config.RegionEndpoint = Amazon.RegionEndpoint.USEast1;
            Amazon.SimpleEmail.AmazonSimpleEmailServiceClient client = new Amazon.SimpleEmail.AmazonSimpleEmailServiceClient("AKIAI6NU4VLLI32AI2UA", Cryptography.Decrypt("NdL0xWLhvbF6pvu3hw1FtluhPmUc1jAuWKgj6mb4V89KztEq6MjEhQoiCPLVv4y9"), config);
            var stream = new MemoryStream();
            MimeMessage objMessage = new MimeMessage();
            string[] emls = email.Split(',');
            foreach (string eml in emls)
            {
                objMessage.To.Add(new MailboxAddress(string.Empty, eml));
            }
            //objMessage.To.Add(new MailboxAddress(string.Empty, email));
            objMessage.From.Add(new MailboxAddress("HorizONE", "mailing@inf.com.mx"));
            objMessage.Subject = subject;
            BodyBuilder emailBodyObj = new BodyBuilder();
            emailBodyObj.HtmlBody = msg;
            //emailBodyObj.Attachments.Add(@"c:\attachment.txt");
            objMessage.Body = emailBodyObj.ToMessageBody();
            objMessage.WriteTo(stream);
            Amazon.SimpleEmail.Model.SendRawEmailRequest mailObj = new Amazon.SimpleEmail.Model.SendRawEmailRequest(new RawMessage(stream));
            SendRawEmailResponse response = client.SendRawEmail(mailObj);
            string res = response.MessageId;

            //System.Net.Mail.MailMessage aMessage = new System.Net.Mail.MailMessage();
            //aMessage.From = new System.Net.Mail.MailAddress("mailing@evoctus.com", "Evoctus Invoice");

            //aMessage.To.Add(email);
            //aMessage.ReplyTo = new System.Net.Mail.MailAddress("mailing@evoctus.com");
            //aMessage.Subject = subject;
            //System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(msg, System.Text.Encoding.UTF8, "text/html");
            //aMessage.IsBodyHtml = true;
            //aMessage.BodyEncoding = System.Text.Encoding.UTF8;


            //aMessage.AlternateViews.Add(htmlView);
            //System.Net.Mail.SmtpClient mailClient = new System.Net.Mail.SmtpClient();
            //mailClient.Host = "smtp.gmail.com";
            //mailClient.Port = 587;
            //mailClient.EnableSsl = true;
            //mailClient.Credentials = new System.Net.NetworkCredential("mailing@inf.com.mx", "m4iling2");

            //mailClient.Send(aMessage);

            //downloadCfdi += "<p>Correo enviado";
            return "success";

        }
        catch (Exception ex)
        {
            return ex.Message;
            //downloadCfdi += "<p><i class=\"w3-red\">No se pudo enviar el correo: +" + ex.Message + "</i>";
        }
    }

    public static string sendInvEmail(string email, string subject, string msg,string attach1, string attach2)
    {
        try
        {
            Amazon.SimpleEmail.AmazonSimpleEmailServiceConfig config = new Amazon.SimpleEmail.AmazonSimpleEmailServiceConfig();
            config.RegionEndpoint = Amazon.RegionEndpoint.USEast1;
            Amazon.SimpleEmail.AmazonSimpleEmailServiceClient client = new Amazon.SimpleEmail.AmazonSimpleEmailServiceClient("AKIAI6NU4VLLI32AI2UA", Cryptography.Decrypt("NdL0xWLhvbF6pvu3hw1FtluhPmUc1jAuWKgj6mb4V89KztEq6MjEhQoiCPLVv4y9"), config);
            var stream = new MemoryStream();
            MimeMessage objMessage = new MimeMessage();
            string[] emls = email.Split(',');
            foreach (string eml in emls)
            {
                objMessage.To.Add(new MailboxAddress(string.Empty, eml));
            }
            objMessage.From.Add(new MailboxAddress("InfnIT Invoice", "mailing@inf.com.mx"));
            objMessage.Subject = subject;
            BodyBuilder emailBodyObj = new BodyBuilder();
            emailBodyObj.HtmlBody = msg;
            emailBodyObj.Attachments.Add(attach1);
            emailBodyObj.Attachments.Add(attach2);
            objMessage.Body = emailBodyObj.ToMessageBody();
            objMessage.WriteTo(stream);
            Amazon.SimpleEmail.Model.SendRawEmailRequest mailObj = new Amazon.SimpleEmail.Model.SendRawEmailRequest(new RawMessage(stream));
            SendRawEmailResponse response = client.SendRawEmail(mailObj);
            string res = response.MessageId;

            System.Net.Mail.MailMessage aMessage = new System.Net.Mail.MailMessage();
            aMessage.From = new System.Net.Mail.MailAddress("mailing@inf.com.mx", "InfnIT Invoice");

            //aMessage.To.Add(email);
            //aMessage.ReplyTo = new System.Net.Mail.MailAddress("facturacion@inf.com.mx");
            //aMessage.Subject = subject;
            //System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(msg, System.Text.Encoding.UTF8, "text/html");
            //aMessage.IsBodyHtml = true;
            //aMessage.BodyEncoding = System.Text.Encoding.UTF8;
            //aMessage.Attachments.Add(new System.Net.Mail.Attachment(attach1));
            //aMessage.Attachments.Add(new System.Net.Mail.Attachment(attach2));

            //aMessage.AlternateViews.Add(htmlView);
            //System.Net.Mail.SmtpClient mailClient = new System.Net.Mail.SmtpClient();
            //mailClient.Host = "smtp.gmail.com";
            //mailClient.Port = 587;
            //mailClient.EnableSsl = true;
            //mailClient.Credentials = new System.Net.NetworkCredential("mailing@inf.com.mx", "m4iling2");

            //mailClient.Send(aMessage);

            return "success";//downloadCfdi += "<p>Correo enviado";

        }
        catch (Exception ex)
        {
            return ex.Message;
            //downloadCfdi += "<p><i class=\"w3-red\">No se pudo enviar el correo: +" + ex.Message + "</i>";
        }
    }

    /// <summary>
    /// Stores a value in a user Cookie, creating it if it doesn't exists yet.
    /// </summary>
    public static void StoreInCookie(
    string cookieName,
    string cookieDomain,
    Dictionary<string, string> keyValueDictionary,
    DateTime? expirationDate,
    bool httpOnly = false)
    {
        // NOTE: we have to look first in the response, and then in the request.
        // This is required when we update multiple keys inside the cookie.
        HttpCookie cookie = HttpContext.Current.Response.Cookies[cookieName]
            ?? HttpContext.Current.Request.Cookies[cookieName];
        if (cookie == null) cookie = new HttpCookie(cookieName);
        if (keyValueDictionary == null || keyValueDictionary.Count == 0)
            cookie.Value = null;
        else
            foreach (var kvp in keyValueDictionary)
                cookie.Values.Set(kvp.Key, kvp.Value);
        if (expirationDate.HasValue) cookie.Expires = expirationDate.Value;
        if (!String.IsNullOrEmpty(cookieDomain)) cookie.Domain = cookieDomain;
        if (httpOnly) cookie.HttpOnly = true;
        HttpContext.Current.Response.Cookies.Set(cookie);
    }

    /// <summary>
    /// Retrieves a single value from Request.Cookies
    /// </summary>
    public static string GetFromCookie(string cookieName, string keyName)
    {
        HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
        if (cookie != null)
        {
            string val = (!String.IsNullOrEmpty(keyName)) ? cookie[keyName] : cookie.Value;
            if (!String.IsNullOrEmpty(val)) return Uri.UnescapeDataString(val);
        }
        return null;
    }



    /// <summary>
    /// Removes a single value from a cookie or the whole cookie (if keyName is null)
    /// </summary>
    public static void RemoveCookie(string cookieName, string keyName, string domain = null)
    {
        if (String.IsNullOrEmpty(keyName))
        {
            if (HttpContext.Current.Request.Cookies[cookieName] != null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
                cookie.Expires = DateTime.UtcNow.AddYears(-1);
                if (!String.IsNullOrEmpty(domain)) cookie.Domain = domain;
                HttpContext.Current.Response.Cookies.Add(cookie);
                HttpContext.Current.Request.Cookies.Remove(cookieName);
            }
        }
        else
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            cookie.Values.Remove(keyName);
            if (!String.IsNullOrEmpty(domain)) cookie.Domain = domain;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }


    /// <summary>
    /// Checks if a cookie / key exists in the current HttpContext.
    /// </summary>
    public static bool CookieExist(string cookieName, string keyName)
    {
        HttpCookieCollection cookies = HttpContext.Current.Request.Cookies;
        return (String.IsNullOrEmpty(keyName))
            ? cookies[cookieName] != null
            : cookies[cookieName] != null && cookies[cookieName][keyName] != null;
    }

    public static string CleanString(string str)
    {
        return str.Replace("é", "e").Replace("á", "a").Replace("í", "i").Replace("ó", "o").Replace("ú", "u").Replace("ñ", "n").Replace("É", "E").Replace("Á", "A").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("Ñ", "N").Replace("ü", "u").Replace("Ü", "U").ToUpper();
    }
    public static string CleanStringn(string str)
    {
        return str.Replace("é", "e").Replace("á", "a").Replace("í", "i").Replace("ó", "o").Replace("ú", "u").Replace("É", "E").Replace("Á", "A").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("ü", "u").Replace("Ü", "U");
    }
}
using Business.Abstract;
using Business.Constans;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Business.Conrete
{
    public class EmailParameterManager : IEmailParameterService
    {
        private readonly IEmailParameterDal _emailParameterDal;

        public EmailParameterManager(IEmailParameterDal emailParameterDal)
        {
            _emailParameterDal = emailParameterDal;
        }

        public IResult Add(EmailParameter emailParameter)
        {
            _emailParameterDal.Add(emailParameter);
            return new SuccessResult(EmailParameterMessage.Added);
        }

        public IResult Delete(EmailParameter emailParameter)
        {
            _emailParameterDal.Delete(emailParameter);
            return new SuccessResult(EmailParameterMessage.Deleted);
        }

        public IDataResult<EmailParameter> GetById(int id)
        {
            var results = _emailParameterDal.GetAll();

            foreach (var result in results)
            {
                if(result.Id == id)
                {
                    return new SuccessDataResult<EmailParameter>(result);
                }
            }
            return new ErrorDataResult<EmailParameter>("bulamadım");

        }

        public IDataResult<List<EmailParameter>> GetList()
        {
            return new SuccessDataResult<List<EmailParameter>>(_emailParameterDal.GetAll());
        }

        public IResult SendEmail(EmailParameter emailParameter, string body, string subject, string emails)
        {
            using (MailMessage mail = new MailMessage())
            {
                string[] setEmails = emails.Split(",");
                mail.From = new MailAddress(emailParameter.Email);
                foreach (var email in setEmails)
                {
                    mail.To.Add(email);
                }
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = emailParameter.Html;
                //mail.Attachments.Add();
                using (SmtpClient smtp = new SmtpClient(emailParameter.SMTP))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(emailParameter.Email, emailParameter.Password);

                    smtp.EnableSsl= emailParameter.SSL;
                    smtp.Port = emailParameter.Port;
                    smtp.Send(mail);

                }
            }
            return new SuccessResult(EmailParameterMessage.SendProccessIsSucc);
        }

        public IResult Update(EmailParameter emailParameter)
        {
            _emailParameterDal.Update(emailParameter);
            return new SuccessResult(EmailParameterMessage.Updated);
        }
    }
}

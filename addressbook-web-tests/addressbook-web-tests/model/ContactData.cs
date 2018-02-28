using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        public string allPhones;
        public string allEmails;
        public string allDetails;

        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Middlename { get; set; }

        public string Nickname { get; set; }

        public string Title { get; set; }

        public string Company { get; set; }
    
        public string Address { get; set; }

        public string Home { get; set; }

        public string Mobile { get; set; }

        public string Work { get; set; }

        public string Fax { get; set; }
        
        public string Email { get; set; }

        public string Email2 { get; set; }

        public string Email3 { get; set; }
        
        public string Homepage { get; set; }

        public string Address2 { get; set; }

        public string Phone2 { get; set; }

        public string Notes { get; set; }

        public string Id { get; set; }

        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (EndStringInsert(Email) + EndStringInsert(Email2) + EndStringInsert(Email3)).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }

        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUpPhones(Home) + CleanUpPhones(Mobile) + CleanUpPhones(Work) + CleanUpPhones(Phone2)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        public string AllDetails
        {
            get
            {
                if (allDetails != null)
                {
                    return allDetails;
                }
                else
                {
                    return (EndStringInsert(EndStringInsert(ContactDetailsList(Firstname, Middlename, Lastname, Nickname, Title, Company, Address)))
                        + EndStringInsert(EndStringInsert(GetTelephoneList(Home, Mobile, Work, Fax)))
                        + EndStringInsert(EndStringInsert(GetEmailList(Email, Email2, Email3, Homepage)))
                        + StartStringInsert(Address2)
                        + EndStringInsert(StartStringInsert(StartStringInsert(StringPhone2(Phone2))))
                        + StartStringInsert(Notes)).Trim();
                }
            }
            set
            {
                allDetails = value;
            }
        }

        public string CleanUpPhones(string phone)
         {
             if (phone == null || phone == "")
             {
                 return "";
             }
            return phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") + "\r\n";
         }

        // Adding a line break to the string beginning
        public string StartStringInsert(string line)
        {
            if (line == null || line == "")
            {
                return "";
            }
            return "\r\n" + line;
        }

        // Adding a line break to the string end
        public string EndStringInsert(string line)
        {
            if (line == null || line == "")
            {
                return "";
            }
            return line + "\r\n";
        }

        // Glue the lines together for fullname as on the details form
        public string GetFullName(string firstname, string middlename, string lastname)
        {
            string form = "";

            if (firstname != null && firstname != "")
            {
                form = Firstname + " ";
            }
            if (middlename != null && middlename != "")
            {
                form = form + Middlename + " ";
            }
            if (lastname != null && lastname != "")
            {
                form = form + Lastname + " ";
            }
            return form.Trim();
        }

        // Glue the lines together for fullname, job and address info as on the details form
        public string ContactDetailsList(string firstname, string middlename, string lastname, string nickname, string title, string company, string address)
        {
            return EndStringInsert(GetFullName(firstname, middlename, lastname)) + EndStringInsert(nickname) + EndStringInsert(title) + EndStringInsert(company) + EndStringInsert(address).Trim();
        }

        // Glue the lines together for telephone list as on the details form
        public string GetTelephoneList(string home, string mobile, string work, string fax)
        {
            string form = "";

            if (home != null && home != "")
            {
                form = form + "H: " + EndStringInsert(Home);
            }
            if (mobile != null && mobile != "")
            {
                form = form + "M: " + EndStringInsert(Mobile);
            }
            if (work != null && work != "")
            {
                form = form + "W: " + EndStringInsert(Work);
            }
            if (fax != null && fax != "")
            {
                form = form + "F: " + EndStringInsert(Fax);
            }
            return form.Trim();
        }

        // Glue the lines together for email list and web info as on the details form
        public string GetEmailList(string email, string email2, string email3, string homepage)
        {
            string form = "";

            if (email != null && email != "")
            {
                form = form + EndStringInsert(email);
            }
            if (email2 != null && email2 != "")
            {
                form = form + EndStringInsert(email2);
            }
            if (email3 != null && email3 != "")
            {
                form = form + EndStringInsert(email3);
            }
            if (homepage != null && homepage != "")
            {
                form = form + EndStringInsert(StringHomePage(homepage));
            }
            return form.Trim();
        }

        // Getting a template for 'homepage' as on the details form
        public string StringHomePage(string homepage)
        {
            if (homepage == null || homepage == "")
            {
                return "";
            }
            return "Homepage:" + "\r\n" + homepage;
        }

        // Getting a template for 'phone2' as on the details form
        public string StringPhone2(string phone2)
        {
            if (phone2 == null || phone2 == "")
            {
                return "";
            }
            return "P: " + Phone2;
        }


        public override int GetHashCode()
        {
            return (Firstname + " " + Lastname).GetHashCode();
        }
        
        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Firstname == other.Firstname && Lastname == other.Lastname;
        }

        public override string ToString()
        {
            return Firstname + " " + Lastname;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return (Firstname + " " + Lastname).CompareTo(other.Firstname + " " + other.Lastname);
        }
    }
}

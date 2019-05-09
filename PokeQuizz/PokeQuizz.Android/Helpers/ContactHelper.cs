using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PokeQuizz.Models;
using PokeQuizz.Services.Interfaces;

namespace PokeQuizz.Droid.Helpers
{
    class ContactHelper : IContacts
    {
        public async Task<List<Contact>> GetDeviceContactsAsync()
        {
            Contact selectedContact = new Contact();
            List<Contact> contactList = new List<Contact>();
            var uri = ContactsContract.CommonDataKinds.Phone.ContentUri;
            string[] projection = { ContactsContract.Contacts.InterfaceConsts.Id, ContactsContract.Contacts.InterfaceConsts.DisplayName, ContactsContract.CommonDataKinds.Phone.Number };
            var cursor = Android.App.Application.Context.ContentResolver.Query(uri, projection, null, null, null);
            if (cursor.MoveToFirst())
            {
                do
                {
                    contactList.Add(new Contact()
                    {
                        DisplayName = cursor.GetString(cursor.GetColumnIndex(projection[1]))
                    });
                } while (cursor.MoveToNext());
            }
            return contactList;
        }
        private object ManagedQuery(Android.Net.Uri uri, string[] projection, object p1, object p2, object p3)
        {
            throw new NotImplementedException();
        }
    }
}
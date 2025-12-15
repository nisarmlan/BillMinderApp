using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BillMinderApp
{
    internal class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://billminderapp-default-rtdb.asia-southeast1.firebasedatabase.app/");

        //add data into database
        public async Task AddRecord(string billName, double amount, string category, string dueDate, string repeat, string note)
        {
            await firebase
            .Child("BillRecords")
            .PostAsync(new BillRecord() { BillName = billName, Amount = amount, Category = category, DueDate = dueDate, Repeat = repeat, Note = note });
        }

        //retrieve data
        public async Task<List<BillRecord>> GetAllBillRecord()
        {
            return (await firebase
            .Child("BillRecords")
            .OnceAsync<BillRecord>()).Select(item => new BillRecord
            {
                BillName = item.Object.BillName,
                Amount = item.Object.Amount,
                Category = item.Object.Category,
                DueDate = item.Object.DueDate,
                Repeat = item.Object.Repeat,
                Note = item.Object.Note
            }).ToList();
        }

        public async Task<BillRecord> GetNearestDueBill()
        {
            var allRecords = await GetAllBillRecord(); // Assume this method retrieves all bills
            return allRecords
                .OrderBy(r => DateTime.ParseExact(r.DueDate, "dd/MM/yyyy", null))
                .FirstOrDefault(r => DateTime.ParseExact(r.DueDate, "dd/MM/yyyy", null) >= DateTime.Today);
        }

    }
}

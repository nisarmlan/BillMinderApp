using System.Windows.Input;

namespace BillMinderApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public MainPage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            displayRecord.ItemsSource = await firebaseHelper.GetAllBillRecord();

            // Fetch the nearest due bill
            var nearestBill = await firebaseHelper.GetNearestDueBill();

            if (nearestBill != null)
            {
                // Find UI elements and update them
                nextBill.Text = $"Next bill: RM {nearestBill.Amount:F2}";
                dueDate.Text = $"Due date: {nearestBill.DueDate}";
            }
            else
            {
                // Default text if no upcoming bills
                nextBill.Text = "Next bill: RM --.--";
                dueDate.Text = "Due date: --/--/----";
            }

        }

        private async void OnAddBillButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddBill());
        }
    }
}

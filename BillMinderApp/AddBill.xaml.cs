namespace BillMinderApp;

public partial class AddBill : ContentPage
{
    FirebaseHelper firebaseHelper = new FirebaseHelper();

    public AddBill()
	{
		InitializeComponent();

	}

    //event handler
    void onDatePickerSelected(object sender, DateChangedEventArgs e)
    {
        var selectedDate = e.NewDate.ToString();
    }

    async void OnSaveRecord(object sender, EventArgs e)
    {
        // Validate required fields: billName and amount
        if (string.IsNullOrWhiteSpace(outputBillName.Text))
        {
            await DisplayAlert("Save Failed", "Bill Name is required.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(outputAmount.Text) ||
            !double.TryParse(outputAmount.Text, out double Amount) || Amount <= 0)
        {
            await DisplayAlert("Save Failed", "Amount is required.", "OK");
            return;
        }

        string billName = outputBillName.Text;
        var amount = Double.Parse(outputAmount.Text);
        string? category = outputCategory.SelectedItem as String ?? "Uncategorized";
        var dueDate = selectDate.Date.ToString("dd/MM/yyyy");
        string? repeat = outputRepeat.SelectedItem as String ?? "No Repeat";
        string note = outputNote.Text;

        // Save record to Firebase
        await firebaseHelper.AddRecord(billName, amount, category, dueDate, repeat, note);
        // Display success alert
        await DisplayAlert("Record Saved", "Bill Record has been saved", "OK");

        // Navigate to MainPage.xaml
        await Navigation.PushAsync(new MainPage());
    }
}
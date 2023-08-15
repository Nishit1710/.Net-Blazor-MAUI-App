namespace Final_Project_Nishit_Patel;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        string connectionString = "server=localhost;port=3306;database=Nishit;user=root;password=password";
        EventsManager eventsManager = new EventsManager(connectionString);
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FyersApiClient
{
    
    // MainWindow.xaml.cs
    public partial class MainWindow : Window
    {
        private FyersApiHelper apiHelper;
        public MainWindow()
        {
            InitializeComponent();

            apiHelper = new FyersApiHelper("api-connect-docs.fyers.in/fyers-lib.js", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJhcGkuZnllcnMuaW4iLCJpYXQiOjE2NDM1NTc3NzAsImV4cCI6MTY0MzU4OTAzMCwibmJmIjoxNjQzNTU3NzcwLCJhdWQiOlsiZDoxIiwiZDoyIl0sInN1YiI6ImFjY2Vzc190b2tlbiIsImF0X2hhc2giOiJnQUFBQUFCaDlyT0s4TkRGbjBLdlVKQ2ZJTXc1ekZheFF3aDhKLVk2YmM1NkZNTUFROEswMXoxSmxlOTUtS1lnNzRzbXhzaF9kZ1ZoQUhwa1FvWUxhSk5JYkJUZEtsS0RFcHk1c3B2bTRhcGhDMDhoZFVTTVc3WT0iLCJkaXNwbGF5X25hbWUiOiJVUEVORFJBIFNJTkdIIiwiZnlfaWQiOiJYVTAwMDMxIiwiYXBwVHlwZSI6MTAwLCJwb2FfZmxhZyI6Ik4ifQ.krq2YDfYdbVE3L2TaqamSMnAFGZ6OCL9AVfz1E28M5E");
            //  populate ComboBoxes with data 
        }

        // Handle button click event 
        private void SendButton(object sender, RoutedEventArgs e)
        {
            // user input 
            string symbol = SymbolComboBox.Text;
            string side = SideComboBox.Text;
            int qty = Convert.ToInt32(QtyTextBox.Text);

            //  JSON payload
            // var payload = new { Symbol = symbol, Side = side, Qty = qty };
            // string jsonPayload = JsonConvert.SerializeObject(payload);
            var payload = new
            {
                Exchange = "NSE",
                Segment = 10,
                InstrumentType = 0,
                ProductType = "INTRADAY",
                OrderType = 2,
                OrderSource = "API",
                Symbol = symbol,
                Side = side,
                Qty = qty
            };

            string jsonPayload = JsonConvert.SerializeObject(payload);


            
            string apiResponse = apiHelper.PlaceOrder(symbol, side, qty, jsonPayload);

          
            LogsTextBox.Text += $"Order Response: {apiResponse}\n";

        }
    }

}

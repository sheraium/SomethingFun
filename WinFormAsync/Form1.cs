using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private async void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = false;

                //var result = await GetDbData();
                //textBox1.Text = result.Value;
                Application.DoEvents();
                //SpinWait.SpinUntil(() => false, 1000);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                timer1.Enabled = true;
            }
        }

        private Task<ShelfData> GetDbData()
        {
            return Task.Run(() =>
            {
                //DB Sql ...
                Thread.Sleep(10_000);
                var shelfData = new ShelfData();
                shelfData.Value = DateTime.Now.ToLongTimeString();
                return shelfData;
            });
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Test: " + Thread.CurrentThread.ManagedThreadId);

            var synchronizationContext = SynchronizationContext.Current;
            await DoSomething();
            //await Task.Yield();

            synchronizationContext = SynchronizationContext.Current;
            textBox1.Text = "ABC";
            Console.WriteLine("Test: " + Thread.CurrentThread.ManagedThreadId);
        }

        private async Task DoSomething()
        {
            Console.WriteLine("Test: " + Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(1000);
            //await Task.Delay(1).ConfigureAwait(false);
            await Task.Yield();
            SynchronizationContext.SetSynchronizationContext(null);
            await Task.Yield();
            Console.WriteLine("Test: " + Thread.CurrentThread.ManagedThreadId);
        }
    }

    internal class ShelfData
    {
        public string Value { get; set; }
    }
}
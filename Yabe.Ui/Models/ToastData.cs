namespace Yabe.Ui.Models
{
    public class ToastData
    {
        public string message { get; set; }
        public bool dismissible { get; set; }
        public bool pauseOnHover { get; set; }
        public string type { get; set; }
        public int duration { get; set; }

        public ToastData()
        {
            dismissible = true;
            pauseOnHover = true;
            duration = 2000;
        }
    }
}

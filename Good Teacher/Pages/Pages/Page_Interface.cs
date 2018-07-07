namespace Good_Teacher.Pages.Pages
{
    public delegate void AddControlDelegate(int Tag);

    interface Page_Interface
    {
        event AddControlDelegate AddControlEvent;
    }
}

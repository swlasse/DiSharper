namespace MvcApplication4.Models
{
    public class Ford : ICar
    {
        public string Name
        {
            get
            {
                return GetType().Name;
            }
        }
    }
}
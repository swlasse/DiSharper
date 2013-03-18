namespace MvcApplication4.Models
{
    public class Ferrari : ICar
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
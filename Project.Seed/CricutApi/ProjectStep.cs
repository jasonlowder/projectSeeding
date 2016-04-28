namespace Project.Seed.CricutApi
{
    public class ProjectStep
    {
        private string _title;
        public string Title
        {
            get
            {
                if (string.IsNullOrEmpty(_title))
                {
                    if (Description.ToLower().Contains("prep"))
                        _title = "PREPARATION";
                    if (Description.ToLower().Contains("print"))
                        _title = "PRINT THEN CUT";
                    if (Description.ToLower().Contains("cut"))
                        _title = "CUT";
                    if (Description.ToLower().Contains("assemble"))
                        _title = "ASSEMBLY";
                }
                return _title;
            }
            set
            {
                _title = value;
            }
        }
        public string Description { get; set; }
        public int Order { get; set; }



        public int Id { get; set; }
    }
}
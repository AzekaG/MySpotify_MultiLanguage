namespace MySpotify.Models
{
    public class SortViewModel
    {
        public SortState NameSort {  get; set; }
        public SortState ArtistSort { get; set; }
        public SortState Current { get; set; }
        public bool Up {  get; set; }

        public SortViewModel(SortState sortOrder)
        {
            NameSort = SortState.NameAsc;
            ArtistSort = SortState.ArtistAsc;
            Up = true;
            if (sortOrder == SortState.NameDesc || sortOrder == SortState.ArtistDesc)
            {
                Up = false;
            }

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    Current = NameSort = SortState.NameAsc;
                    break;
                case SortState.ArtistDesc:
                    Current = ArtistSort = SortState.ArtistAsc;
                    break;
                case SortState.NameAsc:
                    Current = NameSort = SortState.NameDesc;
                    break;
                case SortState.ArtistAsc:
                    Current = NameSort = SortState.ArtistDesc;
                    break;
            }
        }





    }
}

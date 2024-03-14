namespace MySpotify.Models
{
	public class SortViewModel
	{
		public SortState NameSort {  get; set; }  //сортировка по названию песни

		public SortState ArtistSort { get; set;}	//сортировка по имени исполнителя

		public SortState Current {  get; set;}  //значения свойства выбранного ля сортировки

		public bool Up {  get; set;}	//сортирвока по возрастанию или убыванию

		public SortViewModel(SortState sortOrder)
		{
			NameSort = SortState.NameAsc;
			ArtistSort = SortState.ArtistAsc;
			Up = true;


			if(sortOrder == SortState.NameDesc || sortOrder == SortState.ArtistDesc)
			{
				Up = false;
			}

			switch(sortOrder)
			{
				case SortState.NameDesc :
					Current = NameSort = SortState.NameAsc;
					break;
				
				case SortState.ArtistAsc:
					Current = ArtistSort = SortState.ArtistDesc;
					break;
				case SortState.ArtistDesc:
					Current = ArtistSort = SortState.ArtistAsc;
					break;
				default:
					Current = NameSort = SortState.NameDesc;
					break;
			}	
		}

		


	}
}

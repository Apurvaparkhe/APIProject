namespace Infoapi.Models
{
    public class Patent
    {
        public int Id{get; set;}

        public string? Application{get; set;}

         public string? Registration{get; set;}
         public string? Filing_date {get; set;}
        public string? Status {get; set;}
         public string? Status_date {get; set;}
         public string? AppType {get; set;}
         public string? Date_Rec {get; set;}
         public string? PriorityInfo{get; set;}
         
        public string? Holder {get; set;}
         public string? RegDate {get; set;}

        public string? NoOfPatent{ get; set;}
        public string? Representative{get; set;}
        public string? PublicationDate{get; set;}
        public string? Inventor{get; set;}
        public string? Examiner{get; set;}

        

    }

}
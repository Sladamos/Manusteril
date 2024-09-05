namespace Messages
{

    public interface INewPatientRegisteredMessage //Dodanie nowego pacjenta na recepcji
    {
        Guid PatientId { get; }
        string PatientFirstName { get; }
        string PatientLastName { get; }
        string PatientPesel {  get; }
        string PatientCity { get; }
        string PatientAddress { get; }
        string PatientPhoneNumber { get; }
    }

    public interface IPatientDataChangedMessage //Edycja danych pacjenta na recepcji
    {
        Guid PatientId { get; }
        string PatientFirstName { get; }
        string PatientLastName { get; }
        string PatientPesel { get; }
        string PatientCity { get; }
        string PatientAddress { get; }
        string PatientPhoneNumber { get; }
    }

    public interface IPatientVisitRegisteredMessage //Zapytanie o przyjęcie pacjenta z recepcji
    {
        Guid PatientId { get; }
        string PatientPesel { get; }
        Guid VisitId { get; }
        WardType WardType { get; }
    }

    public interface IPatientVisitRegistrationAcceptedResponse //Odpowiedź o możliwości przyjęcia z oddziału
    {
        Guid PatientId { get; }
        string PatientPesel { get; }
        Guid VisitId { get; }
        string WardIdentifier { get; }
    }

    public interface IPatientVisitRegistrationDeclinedResponse //Odpowiedź o niemożności przyjęcia z oddziału
    {
        Guid PatientId { get; }
        string PatientPesel { get; }
        Guid VisitId { get; }
        string Reason { get; }
        string WardIdentifier { get; }
    }

    public interface IPatientVisitAcceptedMessage //Potwierdzenie wysłania pacjenta na oddział
    {
        Guid PatientId { get; }
        string PatientPesel { get; }
        Guid VisitId { get; }
        string WardIdentifier { get; }
    }

    public interface IPatientVisitArrivedMessage //Potwierdzenie przybycia pacjenta na oddział
    {
        Guid PatientId { get; }
        string PatientPesel { get; }
        Guid VisitId { get; }
        string WardIdentifier { get; }
        int Room {  get; }
    }

    public interface IPatientAllowedToLeaveMessage //Pozwolenie na opuszczenie z oddziału
    {
        Guid PatientId { get; }
        string PatientPesel { get; }
        string DoctorPwzNumber { get; }
        bool LeavedAtOwnRisk { get; }
    }

    public interface IPatientVisitUnregisteredMessage //Potwierdzenie wyjścia z recepcji
    {
        Guid PatientId { get; }
        string PatientPesel { get; }
        Guid VisitId { get; }
        DateTime VisitEndDate { get; }
    }

    public interface IPatientWardRoomChangedMessage //Informacja o zmianie sali
    {
        Guid PatientId { get; }
        string PatientPesel { get; }
        string WardIdentifier { get; }
        int Room { get; }
    }

    public interface IIsPatientInsuredMessage
    {
        string PatientPesel { get; }
    }

    public interface IIsPatientInsuredResponse
    {
        string PatientPesel { get; }
        bool IsInsured { get; }
    }
}

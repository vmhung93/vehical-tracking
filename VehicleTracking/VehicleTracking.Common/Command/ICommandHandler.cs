namespace VehicleTracking.Common.Command
{
    public interface ICommandHandler<ICommand, TOutput>
    {
        TOutput Handle(ICommand command);
    }
}

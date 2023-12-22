using CompanionServer.Handlers;
using Facepunch;
using ProtoBuf;

public interface IHandler : IPooled
{
	AppRequest Request { get; }

	ValidationResult Validate ();

	void Execute ();

	void SendError (string code);
}

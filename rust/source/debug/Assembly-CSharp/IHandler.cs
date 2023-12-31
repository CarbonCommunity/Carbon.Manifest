using CompanionServer.Handlers;
using Facepunch;
using ProtoBuf;

public interface IHandler : Pool.IPooled
{
	AppRequest Request { get; }

	ValidationResult Validate ();

	void Execute ();

	void SendError (string code);
}

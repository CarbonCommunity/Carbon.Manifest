using Facepunch;
using ProtoBuf.Nexus;

public interface INexusRequestHandler : IPooled
{
	Response Response { get; }

	void Execute ();
}

using Facepunch;
using ProtoBuf.Nexus;

public interface INexusRequestHandler : Pool.IPooled
{
	Response Response { get; }

	void Execute ();
}

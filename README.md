# RazerChromaConnector

A easy to use Razer Chroma Connect interface for use with .net.

# Usage

Razer Synapse 3 has to be installed to use the connector. You will also need a vaild AppId you can get from [here](https://developer.razer.com/contact/).

 Install the the Nuget-Package ["protoface.ChromaConnector"](https://www.nuget.org/packages/protoface.ChromaConnector/) or download the [source](https://github.com/protoface/RazerChromaConnector) and include ChromaConnector in your project.


```cs
using ChromaConnector;

Guid AppId = Guid.Parse("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx");
ChromaManager.Connect(AppId, Connector_OnBroadcastEvent);

private void Connector_OnBroadcastEvent(object sender, ChromaConnector.Color[] e)
{
    // do stuff
}

// Dispose
ChromaManager.Unitialize();

```
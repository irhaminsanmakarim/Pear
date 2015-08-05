
using StructureMap;
using System;
using System.Threading;
namespace DSLNG.PEAR.Web.DependencyResolution
{

    public static class ObjectFactory
    {
        private static readonly Lazy<Container> ContainerBuilder =
                new Lazy<Container>(DefaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        public static IContainer Container
        {
            get { return ContainerBuilder.Value; }
        }

        private static Container DefaultContainer()
        {
            return new Container(x => x.AddRegistry<DefaultRegistry>());
        }
    }
}
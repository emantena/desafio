namespace DesafioB3.Service.Extensions;

public static class CollectionExtension
{
	public static int TotalPages<T>(this List<T> list, int itensPerPage)
	{
		return (int)Math.Ceiling((double)list.Count / itensPerPage);
	}

	public static List<T> Page<T>(this List<T> list, int currentPage, int itensPerPage)
	{
		var indiceInicial = (currentPage - 1) * itensPerPage;
		var indiceFinal = Math.Min(indiceInicial + itensPerPage, list.Count);

		return list.GetRange(indiceInicial, indiceFinal - indiceInicial);
	}
}
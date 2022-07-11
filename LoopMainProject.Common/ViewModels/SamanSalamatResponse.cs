namespace LoopMainProject.Common.ViewModels;

public class SamanSalamatResponse<T>
{
	#region [Properties]

	public T? Data { get; set; }

	public long RecordsTotal { get; set; }

	public long RecordsFiltered { get; set; }

	public bool IsSuccess { get; set; }

	public short Result { get; set; }

	public string? Message { get; set; }

	public dynamic? ChangedId { get; set; }

	public long CommentsCount { get; set; }

	#endregion
}

public class SamanSalamatResponse
{
	#region [Properties]

	public dynamic? Data { get; set; }

	public long RecordsTotal { get; set; }

	public long RecordsFiltered { get; set; }

	public bool IsSuccess { get; set; }

	public short Result { get; set; }

	public string? Message { get; set; }

	public dynamic? ChangedId { get; set; }


	#endregion
}


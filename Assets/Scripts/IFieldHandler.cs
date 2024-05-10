using System;
public interface IFieldHandler
{
	int CollectionSize { get; }

	CollectionChain Chain { get; }

	bool BeingProcessed { get; }

	event Action<CollectionChain> OnCollectionEnd;

	event Action<CollectionChain> OnCollectionCollapse;

	event Action<CollectionChain> OnSelectionCollapse;

	event Action<CollectionChain> OnItemsVisualCollectionOver;

	void Init(CellField field);

	void NotifyCellMouseDown(Cell cell);

	void NotifyCellMouseUp();

	void NotifyMouseOver(Cell cell);

	void NotifyMouseEnter(Cell cell);

	void NotifyMouseExit(Cell cell);

	bool TryGetAvailableCollection(CellField field, out Cell cell, out Cell matchedCell);
}

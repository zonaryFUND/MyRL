using UnityEngine.UI;

public abstract class TileBase {
    #region Public Interface
    public abstract void InitGraphic(Graphic graphic);
    public abstract bool CanEnterIn(ObjectBase obj);
    public abstract bool CanThroughSlantWise(ObjectBase obj);
    #endregion Public Interface
}

// Type: PetCenter_GCP.ViewEngine.Utils.DisposableAction
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System;

namespace PetCenter_GCP.ViewEngine.Utils
{
  internal class DisposableAction : IDisposable
  {
    private Action _action;

    public DisposableAction(Action action)
    {
      if (action == null)
        throw new ArgumentNullException("action");
      this._action = action;
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      this._action();
    }
  }
}

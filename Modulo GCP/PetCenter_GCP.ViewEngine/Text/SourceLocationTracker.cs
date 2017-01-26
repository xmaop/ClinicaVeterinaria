// Type: PetCenter_GCP.ViewEngine.Text.SourceLocationTracker
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System;

namespace PetCenter_GCP.ViewEngine.Text
{
  public class SourceLocationTracker
  {
    private int _absoluteIndex;
    private int _characterIndex;
    private int _lineIndex;
    private SourceLocation _currentLocation;

    public SourceLocation CurrentLocation
    {
      get
      {
        return this._currentLocation;
      }
      set
      {
        if (!(this._currentLocation != value))
          return;
        this._currentLocation = value;
        this.UpdateInternalState();
      }
    }

    public SourceLocationTracker()
      : this(SourceLocation.Zero)
    {
    }

    public SourceLocationTracker(SourceLocation loc)
    {
      this.CurrentLocation = loc;
      this.UpdateInternalState();
    }

    public void UpdateLocation(char characterRead, Func<char> nextCharacter)
    {
      if (nextCharacter == null)
        throw new ArgumentNullException("nextCharacter");
      ++this._absoluteIndex;
      if ((int) characterRead == 10 || (int) characterRead == 13 && (int) nextCharacter() != 10)
      {
        ++this._lineIndex;
        this._characterIndex = 0;
      }
      else
        ++this._characterIndex;
      this.UpdateLocation();
    }

    public void UpdateLocation(string content)
    {
      for (int i = 0; i < content.Length; ++i)
      {
        Func<char> nextCharacter = (Func<char>) (() => char.MinValue);
        if (i < content.Length - 1)
          nextCharacter = (Func<char>) (() => content[i + 1]);
        this.UpdateLocation(content[i], nextCharacter);
      }
    }

    private void UpdateInternalState()
    {
      this._absoluteIndex = this.CurrentLocation.AbsoluteIndex;
      this._characterIndex = this.CurrentLocation.CharacterIndex;
      this._lineIndex = this.CurrentLocation.LineIndex;
    }

    private void UpdateLocation()
    {
      this.CurrentLocation = new SourceLocation(this._absoluteIndex, this._lineIndex, this._characterIndex);
    }
  }
}

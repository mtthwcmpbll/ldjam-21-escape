using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerStatus : MonoBehaviour {
	
	private bool isDead = false;
	public bool IsDead {
		get {
			return isDead;
		}
		set {
			isDead = value;
			
			if (value) { //dead
				//disable control
				gameObject.GetComponent<CharacterMotor>().canControl = false;
				
				//tilt the user a little to give feedback of death
				gameObject.transform.RotateAround(Vector3.forward, 45.0f);
			} else { //alive again
				//enable control
				gameObject.GetComponent<CharacterMotor>().canControl = true;
				
				//tilt the user a little to give feedback of death
				gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
				
				//move to the last touched checkpoint
				transform.position = CurrentCheckpoint;
				
				//switch back to white
				CurrentColor = ColorCapabilities[0];
			}
		}
	}
	
	public Vector3 CurrentCheckpoint;

	public List<ColorCapability> ColorCapabilities;
	
	private ColorCapability currentColor;
	public ColorCapability CurrentColor {
		get {
			return currentColor;
		}
		set {
			if (!currentColor.Equals(value)) {
				ColorCapability oldColor = currentColor;
				currentColor = value;
				OnColorChanged(this, new ColorChangedEventArgs(oldColor, currentColor));
				if (null != ColorChangeEffect) ColorChangeEffect.Emit();
			}
		}
	}
	
	public ParticleEmitter ColorChangeEffect;
	
	public delegate void ColorChangeHandler(object sender, ColorChangedEventArgs data);
	public event ColorChangeHandler ColorChanged;
	
	protected void OnColorChanged(object sender, ColorChangedEventArgs data) {  
		if(ColorChanged != null) {
	  		ColorChanged(this, data);
		}
	}
	
	public void Start() {
		currentColor = ColorCapabilities[0];
	}
	
}

[Serializable]
public class ColorCapability {
	public string name;
	public Color color;
	public Texture2D indicator;
	public Material material;
	public Material transparentMaterial;
	
	#region Equals and HashCode
	
	public override bool Equals(object obj) {
		// If parameter is null return false.
		if (obj == null)
		{
		    return false;
		}
		
		// If parameter cannot be cast to Point return false.
		ColorCapability t = obj as ColorCapability;
		if ((System.Object)t == null)
		{
		    return false;
		}
		
		return name.Equals(t.name);
	}
	
	public bool Equals(ColorCapability t)
	{
	    // If parameter is null return false:
	    if ((object)t == null)
	    {
	        return false;
	    }
	
	    return name.Equals(t.name);
	}
	
	public override int GetHashCode()
	{
	    unchecked // Overflow is fine, just wrap
	    {
	        int hash = 17;
	
	        hash = hash * 23 + name.GetHashCode();
	
	        return hash;
	    }
	}
	
	#endregion
}

public class ColorChangedEventArgs : EventArgs {
  
	public ColorCapability OldColor { get; internal set; }
	public ColorCapability NewColor { get; internal set; }
	
  	public ColorChangedEventArgs(ColorCapability oldColor, ColorCapability newColor) {  
	    this.OldColor = oldColor;
	    this.NewColor = newColor;
  	}
}

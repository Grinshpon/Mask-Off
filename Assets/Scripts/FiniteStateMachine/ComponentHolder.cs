using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
The manager, on awake on start, will initialize all the states,
passing in the component holder,
and each state will store the components it needs.
*/

public class ComponentHolder
{
  Component[] components;

  public ComponentHolder(Component[] components)
  {
    this.components = components;
  }

  public T Get<T>() where T: Component
  {
    foreach (Component component in components)
    {
      if (component is T)
      {
        T result = component as T;
        return (result);
      }
    }
    return null;
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SimpleContainerView : VisualElement
{
    public event System.Action<object> OnRemove;

    public Foldout foldout;

    private VisualElement contanier;

    public SimpleContainerView(VisualElement element)
    {
        var view = Resources.Load<VisualTreeAsset>("simple_container");
        var root = view.CloneTree().contentContainer;

        // delete button
        var deleteBtn = root.Q<Button>("delete-button");
        deleteBtn.clicked += () => OnRemove?.Invoke(element);

        // content
        contanier = root.Q<VisualElement>("content");
        contanier.Add(element);

        // foldout name
        foldout = root.Q<Foldout>();
        foldout.RegisterValueChangedCallback((evt) =>
        {
            //contanier.style.display = evt.newValue ? DisplayStyle.Flex : DisplayStyle.None;
        });

        Add(root);
    }

    public void SetFoldout(bool value)
    {
        contanier.style.display = value ? DisplayStyle.Flex : DisplayStyle.None;
    }

    public void SetName(string name)
    {
        foldout.text = name;
    }
}
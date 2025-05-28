using Godot;
using System;

public partial class Hud : CanvasLayer
{
    private Node2D _infoPanel;
    private Character _player;
    private TabContainer _tabContainer;
    private Node2D _attributePanel;

    public override void _Ready()
    {
        _infoPanel = GetNode<Node2D>("Info");
        _player = GetTree().CurrentScene.GetNode<Character>("Character");
        _tabContainer = GetNode<TabContainer>("SelectMenu");
        _attributePanel = GetNode<Node2D>("Attributes");
    }

    public override void _Process(double delta)
    {
        _infoPanel.GetNode<Label>("HealthInfo").Text = "Health: " + _player.Health.ToString();
        _infoPanel.GetNode<Label>("EnergyInfo").Text = "Energy: " + _player.Energy.ToString();
        _infoPanel.GetNode<Label>("HungerInfo").Text = "Hunger: " + _player.Hunger.ToString();
        _infoPanel.GetNode<Label>("ThirstInfo").Text = "Thirst: " + _player.Thirst.ToString();
        if (_tabContainer.Visible)
        {
            _attributePanel.Visible = true;
            _attributePanel.GetNode<Label>("IntelligenceInfo").Text = "Intelligence: " + _player.Intelligence.ToString();
            _attributePanel.GetNode<Label>("StrengthInfo").Text = "Strength: " + _player.Strength.ToString();
            _attributePanel.GetNode<Label>("CharismaInfo").Text = "Charisma: " + _player.Charisma.ToString();
            _attributePanel.GetNode<Label>("AgilityInfo").Text = "Agility: " + _player.Agility.ToString();
        }
        else
        {
            _attributePanel.Visible = false;
        }
    }
}

﻿// Copyright 2021 Jamie Taylor
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace ToDew {
    public class OverlayConfig {
        public bool enabled = true;
        public int maxWidth = 600;
        public int maxItems = 10;
        public Color backgroundColor = Color.Black * 0.2f;
        public Color textColor = Color.White * 0.8f;
        public void RegisterConfigMenuOptions(GenericModConfigMenuAPI api, IManifest modManifest) {
            api.RegisterLabel(modManifest, "Overlay", "Configure the always-on overlay showing the list");
            api.RegisterSimpleOption(modManifest, "Enabled", "Is the overlay enabled?", () => this.enabled, (bool val) => this.enabled = val);
            api.RegisterSimpleOption(modManifest, "Max Width", "Maximum width of the overlay in pixels", () => this.maxWidth, (int val) => this.maxWidth = val);
            api.RegisterSimpleOption(modManifest, "Max Items", "Maximum number of items to show in the overlay", () => this.maxItems, (int val) => this.maxItems = val);
        }
    }
    public class ToDoOverlay : IDisposable {
        private readonly ModEntry theMod;
        private readonly ToDoList theList;
        private readonly OverlayConfig config;
        private const string ListHeader = "To-Dew List";
        private const int marginTop = 5;
        private const int marginLeft = 5;
        private const int marginRight = 5;
        private const int marginBottom = 5;
        private const int lineSpacing = 5;
        private readonly SpriteFont font = Game1.smallFont;
        private readonly Vector2 ListHeaderSize;
        private List<String> lines;
        private List<float> lineHeights;
        private Rectangle bounds;
        public ToDoOverlay(ModEntry theMod, ToDoList theList) {
            this.theMod = theMod;
            this.config = theMod.config.overlay;
            this.theList = theList;
            // save "constant" values
            ListHeaderSize = font.MeasureString(ListHeader);
            // initialize rendering callback
            theMod.Helper.Events.Display.RenderedWorld += OnRenderedWorld;
            // initialize the list UI and callback
            theList.OnChanged += OnListChanged;
            syncMenuItemList();
        }

        private void syncMenuItemList() {
            lines = new List<string>();
            lineHeights = new List<float>();
            if (theList.Items.Count == 0) return;
            float availableWidth = Math.Max(config.maxWidth - marginLeft - marginRight, ListHeaderSize.X);
            float usedWidth = ListHeaderSize.X;
            float topPx = marginTop + ListHeaderSize.Y;
            for (int i = 0; i < theList.Items.Count && i < config.maxItems; i++) {
                topPx += lineSpacing;
                string itemText = theList.Items[i].Text;
                var lineSize = font.MeasureString(itemText);
                while (lineSize.X > availableWidth) {
                    if (itemText.Length < 2) {
                        // this really shouldn't happen
                        break;
                    }
                    itemText = itemText.Remove(itemText.Length - 2) + "…";
                    lineSize = font.MeasureString(itemText);
                }
                usedWidth = Math.Max(usedWidth, lineSize.X);
                lines.Add(itemText);
                lineHeights.Add(lineSize.Y);
                topPx += lineSize.Y;
            }
            if (theList.Items.Count > config.maxItems) {
                lines.Add("…");
                float lineHeight = font.MeasureString("…").Y;
                lineHeights.Add(lineHeight);
                topPx += lineHeight;
            }
            bounds = new Rectangle(0, 0, (int)(usedWidth + marginLeft + marginRight), (int)topPx + marginBottom);
        }
        private void OnListChanged(object sender, List<ToDoList.ListItem> e) {
            syncMenuItemList();
        }

        public void Dispose() {
            this.theList.OnChanged -= OnListChanged;
            theMod.Helper.Events.Display.RenderedWorld -= OnRenderedWorld;
        }

        private void OnRenderedWorld(object sender, RenderedWorldEventArgs e) {
            if (lines.Count == 0) return;
            if (!config.enabled) return; // shouldn't get this far, but why not check anyway
            var spriteBatch = e.SpriteBatch;
            spriteBatch.Draw(Game1.fadeToBlackRect, bounds, config.backgroundColor);
            Utility.drawBoldText(spriteBatch, ListHeader, font, new Vector2(marginLeft, marginTop), config.textColor);
            float topPx = marginTop + ListHeaderSize.Y;
            spriteBatch.DrawLine(marginLeft, topPx, new Vector2(ListHeaderSize.X - 3, 1), config.textColor);
            for (int i = 0; i < lines.Count; i++) {
                topPx += lineSpacing;
                spriteBatch.DrawString(font, lines[i], new Vector2(marginLeft, topPx), config.textColor);
                topPx += lineHeights[i];
            }
        }

    }
}

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Graphite.Editor.States {
    public interface IState {
        void ProcessClick ();
    }

    public class State {
        protected Graphite.Core.IDocument _document;

        public State (Graphite.Core.IDocument doc) {
            _document = doc;
        }
    }
    
    public class Adding: State, IState {
        public Adding (Graphite.Core.IDocument doc) : base (doc) {
        }

        public void ProcessClick () {
            _document.CreateVertex ();
        }
    }

    public class Idle: State, IState {
        public Idle (Graphite.Core.IDocument doc) : base (doc) {
        }

        public void ProcessClick () {
            _document.SelectVertex ();
        }
    }

    public class Connecting: State, IState {
        protected Visuals.Vertex _fromVertex;

        public Connecting (Graphite.Core.IDocument doc) : base (doc) {
        }

        public void ProcessClick () {
            _document.SelectVertex ();
            
            Visuals.Vertex thisVisual;

            if ((thisVisual = _document.SelectedVertex()) == null)
                return;

            if (_fromVertex != null)
                _document.ConnectVertexes (_fromVertex, thisVisual);
            else
                _fromVertex = thisVisual;
        }
    }
}
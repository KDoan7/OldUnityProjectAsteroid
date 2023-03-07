using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroid
{
    public class ColorChange : MonoBehaviour
    {
        private Color _originalColor { get; set; }
        private Color _changedColor { get; set; }

        [SerializeField] int howManyFlashes;
        [SerializeField] float howLongToFlash;
        public bool isRunning = false;
        public bool enemyIsRunning = false;
        static float _colorFloat = 1f;
        Fire_Laser firelaser;

        private void Start()
        {
            firelaser = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Fire_Laser>();
        }
        public void SetSpriteRendererColor(GameObject currentGameObject, Color newColor)
        {
            if (currentGameObject is null)
            {
                throw new System.ArgumentNullException(nameof(currentGameObject));
            }
            _changedColor = newColor;
            currentGameObject.GetComponentInChildren<SpriteRenderer>().color = _changedColor;
        }
        public Color GetOriginalSpriteRendererColor(GameObject currentGameObject)
        {
            _originalColor = currentGameObject.GetComponentInChildren<SpriteRenderer>().color;
            return _originalColor;
        }

        public IEnumerator FlashingSpriteRendererColor(GameObject currentGameObject, Color newColor)
        {
            if (currentGameObject.tag == "Enemy" || currentGameObject.tag == "Tank")
            {
                _originalColor = currentGameObject.GetComponentInChildren<SpriteRenderer>().color;
                _changedColor = newColor;
                int counter2 = 0;
                enemyIsRunning = true;

                while (counter2 < howManyFlashes)
                {
                    currentGameObject.GetComponentInChildren<SpriteRenderer>().color = _changedColor;
                    yield return new WaitForSeconds(howLongToFlash);
                    currentGameObject.GetComponentInChildren<SpriteRenderer>().color = _originalColor;
                    yield return new WaitForSeconds(howLongToFlash);
                    counter2++;
                }
                yield return new WaitForSeconds(.05f);
                enemyIsRunning = false;
            }
            else
            {
                _originalColor = currentGameObject.GetComponentInChildren<SpriteRenderer>().color;
                _changedColor = newColor;
                int counter = 0;
                isRunning = true;

                while (counter < howManyFlashes)
                {
                    currentGameObject.GetComponentInChildren<SpriteRenderer>().color = _changedColor;
                    yield return new WaitForSeconds(howLongToFlash);
                    currentGameObject.GetComponentInChildren<SpriteRenderer>().color = _originalColor;
                    yield return new WaitForSeconds(howLongToFlash);
                    counter++;
                }
                isRunning = false;
            }
        }

        public void HoldButtonToSlowlyChangeSpriteRendererColorDarker(GameObject currentGameObject, params bool[] parameters)
        {
            foreach (bool parameter in parameters)
            {
                if (!parameter)
                {
                    return;
                }
            }
            currentGameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(1, _colorFloat, _colorFloat);
            _colorFloat -= .25f * Time.deltaTime;
        }

        public void SlowlyChangeSpriteRendererColorBrighter(GameObject currentGameObject, params bool[] parameters)
        {
            foreach (bool parameter in parameters)
            {
                if (!parameter)
                {
                    return;
                }
            }
            currentGameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(1, _colorFloat, _colorFloat);
            _colorFloat += .50f * Time.deltaTime;
        }

    }
}


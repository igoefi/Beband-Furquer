using UnityEngine;

public class SelectClickPoint : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private Unit _selectedUnit;
    private void Update()
    {
        if (Input.GetMouseButton(1))
            _selectedUnit = null;

        if (!Input.GetMouseButtonDown(0))
            return;

        var ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.SphereCast(ray, 0.5f, out RaycastHit hit))
        {
            var obj = hit.collider.gameObject;

            var unit = obj.GetComponent<Unit>();
            var build = obj.GetComponent<Build>();
            var enemy = obj.GetComponent<EnemyUnit>();

            if (unit != null)
                SetUnit(unit);

            else if (build != null)
                if (_selectedUnit != null)
                    _selectedUnit.SetPointToGo(build);
                else
                    build.Click();

            else if (enemy != null)
                if (_selectedUnit != null)
                    _selectedUnit.SetPointToGo(enemy);
                else
                    enemy.Click();

            else
                if (_selectedUnit != null)
                    _selectedUnit.SetPointToGo(hit.point);
        }
    }

    private void SetUnit(Unit unit)
    {
        _selectedUnit = unit;
        unit.Click();
    }
}

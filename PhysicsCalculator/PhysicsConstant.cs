// ================================================================
// MODEL: Physics Constant  –  .NET 10
// ================================================================
namespace PhysicsCalculator;

// ── Record with primary constructor ────────────────────────────
public sealed record PhysicsConstant(
    string Symbol,
    string Name,
    double Value,
    string Unit,
    bool   IsCustom = false)
{
    // Limited mutability: only this field may change
    public bool IsCustom { get; set; } = IsCustom;
}

// ================================================================
// MANAGER: Singleton for managing the constants list
// ================================================================
public sealed class ConstantsManager
{
    // ── Thread-safe singleton via Lazy<T> ──────────────────────
    private static readonly Lazy<ConstantsManager> _lazy =
        new(() => new ConstantsManager());

    public static ConstantsManager Instance => _lazy.Value;

    private readonly List<PhysicsConstant> _list = [];

    private ConstantsManager() { }

    // ── Load built-in constants (CODATA 2022) ──────────────────
    public void LoadDefaults()
    {
        _list.Clear();
        _list.AddRange(
        [
            new("c",        "Speed of light",                   2.99792458e8,       "m/s"),
            new("h",        "Planck constant",                  6.62607015e-34,     "J\u00B7s"),
            new("\u210F",   "Reduced Planck constant",          1.054571817e-34,    "J\u00B7s"),
            new("G",        "Gravitational constant",           6.67430e-11,        "N\u00B7m\u00B2/kg\u00B2"),
            new("g",        "Standard gravity",                 9.80665,            "m/s\u00B2"),
            new("k_B",      "Boltzmann constant",               1.380649e-23,       "J/K"),
            new("N_A",      "Avogadro's number",                6.02214076e23,      "mol\u207B\u00B9"),
            new("e",        "Elementary charge",                1.602176634e-19,    "C"),
            new("\u03B5\u2080", "Permittivity of free space",   8.8541878128e-12,   "F/m"),
            new("\u03BC\u2080", "Permeability of free space",   1.25663706212e-6,   "N/A\u00B2"),
            new("R",        "Molar gas constant",               8.314462618,        "J/(mol\u00B7K)"),
            new("m_e",      "Electron mass",                    9.1093837015e-31,   "kg"),
            new("m_p",      "Proton mass",                      1.67262192369e-27,  "kg"),
            new("m_n",      "Neutron mass",                     1.67492749804e-27,  "kg"),
            new("\u03C3",   "Stefan-Boltzmann constant",        5.670374419e-8,     "W/(m\u00B2\u00B7K\u2074)"),
            new("a\u2080",  "Bohr radius",                      5.29177210903e-11,  "m"),
            new("\u03B1",   "Fine-structure constant",          7.2973525693e-3,    "(dimensionless)"),
            new("R\u221E",  "Rydberg constant",                 1.0973731568160e7,  "m\u207B\u00B9"),
            new("F",        "Faraday constant",                 96485.33212,        "C/mol"),
            new("u",        "Atomic mass unit",                 1.66053906660e-27,  "kg"),
        ]);
    }

    // ── CRUD ────────────────────────────────────────────────────
    public IReadOnlyList<PhysicsConstant> AllConstants() => _list;

    public void Add(PhysicsConstant c)
    {
        _list.Add(c with { IsCustom = true });
    }

    public void Update(int idx, PhysicsConstant c)
    {
        if ((uint)idx >= (uint)_list.Count) return;
        _list[idx] = c with { IsCustom = true };
    }

    public void Delete(int idx)
    {
        if ((uint)idx >= (uint)_list.Count) return;
        if (_list[idx].IsCustom)
            _list.RemoveAt(idx);
    }

    public PhysicsConstant? Get(int idx) =>
        ((uint)idx < (uint)_list.Count) ? _list[idx] : null;
}

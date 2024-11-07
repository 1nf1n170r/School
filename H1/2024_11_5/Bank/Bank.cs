class Bank(Dictionary<uint, Account> accounts)
{
    private Dictionary<uint, Account> m_accounts = accounts;
    public uint AddAccount(string fornavn, string efternavn, Admin admin)
    {
        var id = (uint)this.m_accounts.Count;
        this.m_accounts.Add(id, new Account(new Owner(fornavn, efternavn, id), admin));
        return id;
    }
    public void DeleteAccount(uint id)
    {
        this.m_accounts.Remove(id);
    }
    public float UpdateAccountBallance(uint id, float value)
    {
        var account = this.m_accounts[id];
        account.m_ballance += value;
        return account.m_ballance;
    }
}

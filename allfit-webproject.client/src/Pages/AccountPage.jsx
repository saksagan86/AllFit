import { useAuth } from '../context/AuthContext.jsx';

function AccountPage() {
  const { user } = useAuth();

  return (
    <main className="page-container">
      <section className="auth-card">
        <h1>Mijn account</h1>
        <p>Welkom in het ledenportaal van AllFit.</p>

        <div className="account-details">
          <p>
            <strong>Naam:</strong> {user?.name || 'Onbekend'}
          </p>
          <p>
            <strong>E-mailadres:</strong> {user?.email || 'Onbekend'}
          </p>
        </div>
      </section>
    </main>
  );
}

export default AccountPage;
import { useState } from 'react';

const initialForm = {
    naam: '',
    email: '',
    titel: '',
    beschrijving: '',
    website: ''
};

function ContactPage() {
    const [form, setForm] = useState(initialForm);
    const [errors, setErrors] = useState({});
    const [status, setStatus] = useState({ type: '', message: '' });
    const [isSubmitting, setIsSubmitting] = useState(false);

    const handleChange = (event) => {
        const { name, value } = event.target;

        setForm((previous) => ({
            ...previous,
            [name]: value
        }));
    };

    const validate = () => {
        const newErrors = {};

        if (form.naam.trim().length < 2) {
            newErrors.naam = 'Vul minimaal 2 tekens in.';
        }

        if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(form.email.trim())) {
            newErrors.email = 'Vul een geldig e-mailadres in.';
        }

        if (form.titel.trim().length < 3) {
            newErrors.titel = 'Vul minimaal 3 tekens in.';
        }

        if (form.beschrijving.trim().length < 10) {
            newErrors.beschrijving = 'Vul minimaal 10 tekens in.';
        }

        if (form.beschrijving.length > 2000) {
            newErrors.beschrijving = 'Je bericht mag maximaal 2000 tekens bevatten.';
        }

        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        setStatus({ type: '', message: '' });

        if (!validate()) {
            setStatus({
                type: 'error',
                message: 'Controleer de velden en probeer het opnieuw.'
            });
            return;
        }

        try {
            setIsSubmitting(true);

            const response = await fetch('/api/contact', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(form)
            });

            if (response.status === 429) {
                throw new Error('Je hebt te vaak een bericht verstuurd. Probeer het later opnieuw.');
            }

            if (!response.ok) {
                throw new Error('Het bericht kon niet worden verstuurd. Probeer het opnieuw.');
            }

            const data = await response.json();

            setStatus({
                type: 'success',
                message: data.bericht
            });

            setForm(initialForm);
            setErrors({});
        } catch (error) {
            setStatus({
                type: 'error',
                message: error.message
            });
        } finally {
            setIsSubmitting(false);
        }
    };

    return (
        <main className="contact-page">
            <section className="contact-hero">
                <p className="contact-eyebrow">Contact</p>
                <h1>Neem contact op met AllFit</h1>
                <p>
                    Heb je vragen over lid worden, toegankelijk sporten, groepslessen of onze locaties
                    in Delft en Den Haag? Stuur ons een bericht.
                </p>
            </section>

            <section className="contact-layout">
                <form className="contact-form" onSubmit={handleSubmit} noValidate>
                    <div className="form-group">
                        <label htmlFor="naam">Naam</label>
                        <input
                            id="naam"
                            name="naam"
                            type="text"
                            value={form.naam}
                            onChange={handleChange}
                            aria-describedby={errors.naam ? 'naam-error' : undefined}
                            required
                        />
                        {errors.naam && <span id="naam-error" className="field-error">{errors.naam}</span>}
                    </div>

                    <div className="form-group">
                        <label htmlFor="email">E-mailadres</label>
                        <input
                            id="email"
                            name="email"
                            type="email"
                            value={form.email}
                            onChange={handleChange}
                            aria-describedby={errors.email ? 'email-error' : undefined}
                            required
                        />
                        {errors.email && <span id="email-error" className="field-error">{errors.email}</span>}
                    </div>

                    <div className="form-group">
                        <label htmlFor="titel">Onderwerp</label>
                        <input
                            id="titel"
                            name="titel"
                            type="text"
                            value={form.titel}
                            onChange={handleChange}
                            aria-describedby={errors.titel ? 'titel-error' : undefined}
                            required
                        />
                        {errors.titel && <span id="titel-error" className="field-error">{errors.titel}</span>}
                    </div>

                    <div className="form-group">
                        <label htmlFor="beschrijving">Bericht</label>
                        <textarea
                            id="beschrijving"
                            name="beschrijving"
                            rows="7"
                            value={form.beschrijving}
                            onChange={handleChange}
                            aria-describedby={errors.beschrijving ? 'beschrijving-error' : undefined}
                            required
                        />
                        {errors.beschrijving && (
                            <span id="beschrijving-error" className="field-error">
                                {errors.beschrijving}
                            </span>
                        )}
                    </div>

                    <div className="honeypot" aria-hidden="true">
                        <label htmlFor="website">Website</label>
                        <input
                            id="website"
                            name="website"
                            type="text"
                            value={form.website}
                            onChange={handleChange}
                            tabIndex="-1"
                            autoComplete="off"
                        />
                    </div>

                    {status.message && (
                        <div className={`form-feedback ${status.type}`} role="status">
                            {status.message}
                        </div>
                    )}

                    <button type="submit" className="contact-submit" disabled={isSubmitting}>
                        {isSubmitting ? 'Versturen...' : 'Bericht versturen'}
                    </button>
                </form>

                <aside className="contact-info" aria-label="Contactinformatie">
                    <h2>Waarvoor kun je ons bereiken?</h2>
                    <ul>
                        <li>Vragen over lidmaatschappen en proeflessen</li>
                        <li>Informatie over sporten met een beperking</li>
                        <li>Vragen over groepslessen, kickboksen en fitness</li>
                        <li>Algemene vragen over locaties en faciliteiten</li>
                    </ul>
                </aside>
            </section>
        </main>
    );
}

export default ContactPage;